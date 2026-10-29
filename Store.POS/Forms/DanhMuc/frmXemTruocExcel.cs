using System.Data;
namespace Store.POS;
public partial class frmXemTruocExcel:Form
{
    public frmXemTruocExcel():this(new DataTable(),new List<string>()) {}
    public frmXemTruocExcel(DataTable data,List<string> errors)
    {
        InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);PosFormStyle.ConfigureGridSelection(this,layout,null);dgvDuLieu.DataSource=data;
        if(data.Columns.Count>0) foreach(DataGridViewColumn column in dgvDuLieu.Columns) column.Visible=data.Columns.Contains(column.DataPropertyName);txtLoi.Text=string.Join(Environment.NewLine,errors);
        lblKetQua.Text=$"{data.Rows.Count} dòng hợp lệ; {errors.Count} lỗi. Kiểm tra trước khi nhập.";
        btnNhap.Enabled=data.Rows.Count>0;btnNhap.Text=$"Nhập {data.Rows.Count} dòng hợp lệ";
    }
}
