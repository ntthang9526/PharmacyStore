using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Store.DAL;
using Store.BLL;
using Store.POS;
internal static partial class Program
{
    static void TestAdminProtection()
    {
        int admin=PhanQuyenDAL.NhanVienId;
        long version=DBConnection.DataVersion;
        var bll=new NhanVienBLL();
        Check(!bll.Khoa(admin,admin,out var error) && error.Contains("tài khoản Admin"),"public lock rejects Admin without saving");
        Check(Count("SELECT COUNT(*) FROM Employees WHERE Id=@id AND IsActive=1",("@id",admin))==1 && DBConnection.DataVersion==version,"blocked Admin lock leaves account and data version unchanged");
        using(var connection=DBConnection.GetDbConnection())
        using(var transaction=connection.BeginTransaction(IsolationLevel.Serializable))
        {
            int Add(string role)
            {
                using var cmd=new SqlCommand("INSERT INTO Employees(FullName,Username,PasswordHash,IsActive,Role) OUTPUT INSERTED.Id VALUES(N'Protection test',@u,'unused',1,@r)",connection,transaction);
                cmd.Parameters.AddWithValue("@u","PROTECT_"+Guid.NewGuid().ToString("N"));cmd.Parameters.AddWithValue("@r",role);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            int secondAdmin=Add("ADMIN"),staff=Add("STAFF");
            var validate=typeof(NhanVienDAL).GetMethod("KiemTraTaiKhoan",PrivateStatic)!;
            void Denied(int? id,string? role,string? username,string reason)
            {
                bool rejected=false;
                try {validate.Invoke(null,new object?[]{connection,transaction,id,false,admin,role,username});}
                catch(TargetInvocationException ex) when(ex.InnerException is InvalidOperationException invalid && invalid.Message.Contains("tài khoản Admin")){rejected=true;}
                Check(rejected,reason);
            }
            Denied(secondAdmin,null,null,"another Admin cannot be locked even when multiple Admins exist");
            Denied(secondAdmin,"STAFF","renamed","editing role/name and disabling Admin in one save is rejected");
            Denied(null,"ADMIN","new_admin","cannot create an inactive Admin");
            Denied(staff,"ADMIN","promoted_admin","cannot promote an inactive account to Admin");
            Denied(null,"STAFF","ADMIN","reserved admin username cannot be inactive");
            validate.Invoke(null,new object?[]{connection,transaction,staff,false,admin,null,null});
            Check(true,"ordinary employee can still be deactivated");
            transaction.Rollback();
        }
        using var form=new frmNhanVien();form.Site=new FormDesignSite(form);Offscreen(form);
        var data=new DataTable();data.Columns.Add("MaNV",typeof(int));data.Columns.Add("HoTen");data.Columns.Add("TaiKhoan");data.Columns.Add("DienThoai");data.Columns.Add("VaiTro");data.Columns.Add("DangLam",typeof(bool));
        data.Rows.Add(900001,"Admin test","another_admin","","ADMIN",true);data.Rows.Add(900002,"Staff test","ordinary_staff","","STAFF",true);
        var grid=UiField<DataGridView>(form,"dgvNhanVien");grid.DataSource=data;
        void Select(int index)=>InvokePrivate(form,"Chon",grid.Rows[index]);
        var active=UiField<CheckBox>(form,"chkDangLam");var lockButton=UiField<Button>(form,"btnKhoa");
        Select(0);Check(!lockButton.Enabled,"Admin row has lock button disabled");
        UiField<Button>(form,"btnSua").PerformClick();Check(active.Checked && !active.Enabled,"editing Admin keeps active checked and locked");
        UiField<ComboBox>(form,"cboVaiTro").SelectedIndex=0;Check(active.Checked && !active.Enabled,"changing role in editor does not bypass original Admin protection");
        InvokePrivate(form,"ResetDetails",true);Select(1);Check(lockButton.Enabled,"ordinary employee keeps the lock action");
        UiField<Button>(form,"btnSua").PerformClick();Check(active.Enabled,"ordinary employee activity remains editable");
        active.Checked=false;UiField<ComboBox>(form,"cboVaiTro").SelectedIndex=1;Check(active.Checked && !active.Enabled,"choosing Admin automatically requires active account");
        UiField<ComboBox>(form,"cboVaiTro").SelectedIndex=0;Check(active.Enabled,"switching a staff editor back restores activity editing");
        Check(DBConnection.DataVersion==version,"checks leave no committed employee changes");
    }
}
