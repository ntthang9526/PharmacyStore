using System.Data;
using Store.BLL;
namespace Store.POS;
public partial class frmHoaDon : Form, ILamMoiDuLieu
{
    private readonly HoaDonBLL bll=new();
    private readonly int? maMoSan;
    private int? hoaDonDangXem;
    private readonly ToolTip chiTietHint;
    private string trangThaiDanhSach="";
    private readonly (SizeType Type,float Height)[] detailRows;

    public frmHoaDon():this(null) {}
    public frmHoaDon(int? id)
    {
        maMoSan=id;
        InitializeComponent();
        detailRows=receiptColumns.RowStyles.Cast<RowStyle>().Select(style=>(style.SizeType,style.Height)).ToArray();
        chiTietHint=new ToolTip(components ??= new System.ComponentModel.Container());
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
        dgvHoaDon.AutoGenerateColumns = false; dgvChiTiet.AutoGenerateColumns = false;
        dgvHoaDon.CellFormatting+=(_,e)=>
        {
            if(e.RowIndex<0 || e.ColumnIndex<0 || dgvHoaDon.Rows[e.RowIndex].DataBoundItem is not DataRowView invoice) return;
            if(dgvHoaDon.Columns[e.ColumnIndex].DataPropertyName=="TrangThai")
                e.CellStyle!.ForeColor=(string)invoice["PaymentStatus"] switch
                {
                    "PAID"=>Color.FromArgb(21,128,61),
                    "PENDING"=>Color.FromArgb(180,83,9),
                    _=>Color.FromArgb(100,116,139)
                };
        };
        cboHinhThuc.SelectedIndex=0;
        btnInPdf.Click+=(_,_)=>InHoaDon();
        lblChiTiet.LinkClicked+=(_,_)=>XemDonThuoc();
        cboHinhThuc.SelectedIndexChanged+=(_,_)=>btnThanhToanCho.Enabled=cboHinhThuc.SelectedIndex>=0;
        btnXuatExcel.Click+=(_,_)=>XuatDanhSachExcel();btnXuatChiTietExcel.Click+=(_,_)=>XuatChiTietExcel();
        btnThanhToanCho.Click+=(_,_)=>ThanhToanCho();chkChiCho.CheckedChanged+=(_,_)=>TimKiem();
        btnSuaCho.Click+=(_,_)=>SuaDonCho();btnHuyCho.Click+=(_,_)=>HuyDonCho();
        PosFormStyle.ConfigureRowNavigation(dgvHoaDon,HienChiTiet);
        PosFormStyle.ConfigureGridSelection(this, posLayout, XoaChiTiet);
        dgvHoaDon.CellClick+=(_,e)=> {if(e.RowIndex>=0) HienChiTiet(dgvHoaDon.Rows[e.RowIndex]);};
        
        btnTimKiem.Click+=(_,_)=>TimKiem();
        txtTimKiem.KeyDown+=(_,e)=> {if(e.KeyCode==Keys.Enter) {e.SuppressKeyPress=true;TimKiem();}};
        btnLamMoi.Click+=(_,_)=> {txtTimKiem.Clear();dtpTuNgay.Checked=dtpDenNgay.Checked=chkChiCho.Checked=false;TimKiem();};
        Load+=(_,_)=>
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            dtpTuNgay.Value=DateTime.Today.AddMonths(-1); dtpDenNgay.Value=DateTime.Today;
            dtpTuNgay.Checked=dtpDenNgay.Checked=false;
            TimKiem();
            if(maMoSan.HasValue)
            {
                var row=dgvHoaDon.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>Convert.ToInt32(r.Cells["MaHD"].Value)==maMoSan.Value);
                if(row!=null) HienChiTiet(row);
            }
        };
    }
    private void XoaChiTiet()
    {
        hoaDonDangXem=null;btnXuatChiTietExcel.Enabled=false;btnInPdf.Enabled=false;lblChiTiet.LinkArea=new LinkArea(0,0);lblChiTiet.TabStop=false;lblHinhThuc.Visible=btnThanhToanCho.Visible=cboHinhThuc.Visible=btnSuaCho.Visible=btnHuyCho.Visible=false;
        dgvChiTiet.DataSource=null;
        chiTietHint.SetToolTip(lblChiTiet,"");
        lblChiTiet.Text="Chọn hóa đơn trong bảng để xem các thuốc đã bán.";
        lblTrangThai.Text=trangThaiDanhSach;
        HienVungChiTiet(false);
    }
    private bool timKiemLai;
    private async void TimKiem()
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        if(!Enabled) {timKiemLai=true;return;}
        timKiemLai=false;
        string keyword=txtTimKiem.Text;
        DateTime? from=dtpTuNgay.Checked?dtpTuNgay.Value:null,to=dtpDenNgay.Checked?dtpDenNgay.Value:null;
        bool pending=chkChiCho.Checked;int? selected=hoaDonDangXem??maMoSan;
        lblTrangThai.Text="Đang tải hóa đơn…";
        await PosBackground.LoadAsync(this,()=>bll.TimKiem(keyword,from,to,pending),rows=>
        {
            XoaChiTiet();dgvHoaDon.DataSource=rows;
            decimal total=rows.Rows.Cast<DataRow>().Sum(row=>(decimal)row["ThucThu"]);
            lblTrangThai.Text=trangThaiDanhSach=$"{rows.Rows.Count} hóa đơn · Tổng thực thu: {total:N0} đ";
            lblTrangThai.ForeColor=SystemColors.ControlText;
            if(selected.HasValue)
            {
                var row=dgvHoaDon.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>(int)r.Cells["MaHD"].Value==selected.Value);
                if(row!=null) HienChiTiet(row);
            }
        },error=> {lblTrangThai.ForeColor=Color.Firebrick;lblTrangThai.Text="Không tải được hóa đơn: "+error.Message;});
        if(!IsDisposed && timKiemLai) TimKiem();
    }

    private void HienChiTiet(DataGridViewRow row)
    {
        if(row.DataBoundItem is not DataRowView data) return;
        try
        {
            hoaDonDangXem=(int)data["MaHD"];
            HienVungChiTiet(true);
            btnXuatChiTietExcel.Enabled=true;btnInPdf.Enabled=(string)data["PaymentStatus"]=="PAID";lblHinhThuc.Visible=btnThanhToanCho.Visible=cboHinhThuc.Visible=btnSuaCho.Visible=btnHuyCho.Visible=(string)data["PaymentStatus"]=="PENDING";
            dgvChiTiet.DataSource=bll.ChiTiet((int)data["MaHD"]);
            lblChiTiet.Text=$"HD{(int)data["MaHD"]:D5} · {data["TrangThai"]} · {data["HinhThucThanhToan"]}\nNV lập: {data["NhanVien"]} · NV thu: {data["NhanVienThuTien"]}\nGiá trị hóa đơn: {data["GiaTriHoaDon"]:N0} đ · Đã thu: {data["ThucThu"]:N0} đ · Còn phải thu: {data["CanThanhToan"]:N0} đ";
            lblChiTiet.LinkArea=new LinkArea(0,0);lblChiTiet.TabStop=false;
            if(!data.Row.IsNull("PrescriptionNumber"))
            {
                const string link="Đơn thuốc (bấm xem)";
                lblChiTiet.Text=lblChiTiet.Text.Insert(lblChiTiet.Text.IndexOf('\n')," · "+link);
                lblChiTiet.LinkArea=new LinkArea(lblChiTiet.Text.IndexOf(link,StringComparison.Ordinal),link.Length);lblChiTiet.TabStop=true;
            }
            chiTietHint.SetToolTip(lblChiTiet,$"Tiền hàng: {data["TongTien"]:N0} đ · Giảm: {data["GiamGia"]:N0} đ\n{data["NguonDauHoaDon"]}");
            lblTrangThai.Text=trangThaiDanhSach+(((string)data["NguonDauHoaDon"]).StartsWith("Chưa")?" · Chứng từ cũ: tên thiếu lấy từ danh mục hiện tại.":"");
            if((string)data["PaymentStatus"]=="CANCELLED")
            {
                string reason=Convert.ToString(data["CancelReason"])??"";
                string preview=reason.Replace("\r"," ").Replace("\n"," ");
                if(preview.Length>48) preview=preview[..48]+"…";
                string employee=Convert.ToString(data["NhanVienHuy"])??"";
                if(employee.Length>32) employee=employee[..32]+"…";
                lblChiTiet.Text+=$"\nHủy: {employee} · {data["CancelledAt"]:dd/MM/yyyy HH:mm} · Lý do: {preview}";
                chiTietHint.SetToolTip(lblChiTiet,"Lý do hủy: "+reason);
            }
            else if(!data.Row.IsNull("ModifiedAt"))
                lblChiTiet.Text+=$"\nSửa: {data["NhanVienSua"]} · {data["ModifiedAt"]:dd/MM/yyyy HH:mm}";
        }
        catch(Exception ex) {XoaChiTiet();lblTrangThai.Text="Không tải được chi tiết: "+ex.Message;}
    }

    private void HienVungChiTiet(bool visible)
    {
        receiptColumns.SuspendLayout();
        groupBox14.Visible=visible;
        for(int i=0;i<detailRows.Length;i++)
        {
            receiptColumns.RowStyles[i].SizeType=visible?detailRows[i].Type:SizeType.Percent;
            receiptColumns.RowStyles[i].Height=visible?detailRows[i].Height:(i==0?100:0);
        }
        receiptColumns.ResumeLayout();
    }
}
