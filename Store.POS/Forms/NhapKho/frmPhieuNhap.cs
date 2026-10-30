using System.Data;
using Store.BLL;
namespace Store.POS;
public partial class frmPhieuNhap : Form,ILamMoiDuLieu
{
    private readonly PhieuNhapBLL bll=new();
    private int? selected;
    private readonly (SizeType Type,float Height)[] detailRows;
    public frmPhieuNhap()
    {
        InitializeComponent();
        detailRows=receiptColumns.RowStyles.Cast<RowStyle>().Select(style=>(style.SizeType,style.Height)).ToArray();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);PosFormStyle.ConfigureRowNavigation(dgvPhieuNhap,row=>Chon(row));
        PosFormStyle.ConfigureGridSelection(this,posLayout,XoaChiTiet);
        btnTimKiem.Click+=(_,_)=>TimKiem();btnLamMoi.Click+=(_,_)=> {txtTimKiem.Clear();dtpTuNgay.Checked=dtpDenNgay.Checked=false;TimKiem();};
        txtTimKiem.KeyDown+=(_,e)=> {if(e.KeyCode==Keys.Enter) {e.SuppressKeyPress=true;TimKiem();}};
        dgvPhieuNhap.CellClick+=(_,e)=> {if(e.RowIndex>=0) Chon(dgvPhieuNhap.Rows[e.RowIndex]);};
        
        Load+=(_,_)=>TimKiem();
    }
    private void XoaChiTiet() {selected=null;dgvChiTiet.DataSource=null;lblChiTiet.Text="Chọn phiếu để xem chi tiết nhập hàng.";HienVungChiTiet(false);}
    private void TimKiem(bool throwOnError=false)
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        try
        {
            var data=bll.TimKiem(txtTimKiem.Text,dtpTuNgay.Checked?dtpTuNgay.Value:null,dtpDenNgay.Checked?dtpDenNgay.Value:null);
            XoaChiTiet();dgvPhieuNhap.DataSource=data;lblTrangThai.ForeColor=SystemColors.ControlText;
            lblTrangThai.Text=$"{data.Rows.Count} phiếu nhập · Tổng tiền: {data.Rows.Cast<DataRow>().Sum(r=>(decimal)r["TongTien"]):N0} đ";
        }
        catch(Exception ex) {if(throwOnError) throw;lblTrangThai.ForeColor=Color.Firebrick;lblTrangThai.Text=ex.Message;}
    }
    private void Chon(DataGridViewRow row,bool throwOnError=false)
    {
        if(row.DataBoundItem is not DataRowView data) return;
        try
        {
            selected=(int)data["MaPhieu"];dgvChiTiet.DataSource=bll.ChiTiet(selected.Value);
            HienVungChiTiet(true);
            lblChiTiet.Text=$"PN{selected:D5} · {data["NgayNhap"]:dd/MM/yyyy HH:mm} · NV: {data["NhanVien"]} · NCC: {data["TenNCC"]}\nTổng tiền: {data["TongTien"]:N0} đ · Ghi chú: {data["GhiChu"]}";
        }
        catch(Exception ex) {if(throwOnError) throw;XoaChiTiet();lblTrangThai.Text=ex.Message;}
    }
    private void HienVungChiTiet(bool visible)
    {
        receiptColumns.SuspendLayout();
        grpChiTiet.Visible=visible;
        for(int i=0;i<detailRows.Length;i++)
        {
            receiptColumns.RowStyles[i].SizeType=visible?detailRows[i].Type:SizeType.Percent;
            receiptColumns.RowStyles[i].Height=visible?detailRows[i].Height:(i==0?100:0);
        }
        receiptColumns.ResumeLayout();
    }
    public void LamMoiDuLieu()
    {
        int? id=selected;TimKiem(throwOnError:true);
        if(id.HasValue)
        {
            var row=dgvPhieuNhap.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>(int)r.Cells["MaPhieu"].Value==id);
            if(row!=null) Chon(row,throwOnError:true);
        }
    }
}
