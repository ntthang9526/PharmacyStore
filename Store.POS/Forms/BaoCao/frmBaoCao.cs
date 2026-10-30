using System.Data;
using System.Globalization;
using Store.BLL;

namespace Store.POS
{
    public partial class frmBaoCao : Form, ILamMoiDuLieu
    {
        private readonly BaoCaoBLL bll = new BaoCaoBLL();
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");

        public frmBaoCao()
        {
            InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            btnXuatExcel.Click+=(_,_)=>XuatExcel();
            PosFormStyle.ConfigureGridSelection(this, posLayout, null);
        }

        private async void frmBaoCao_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            DateTime homNay = DateTime.Today;
            dtpTuNgay.Value = homNay.AddDays(-30);   // Mặc định: 30 ngày gần nhất để luôn xem được các hóa đơn gần đây
            dtpDenNgay.Value = homNay;
            await XemBaoCaoAsync(false);
        }

        private async void btnXem_Click(object sender, EventArgs e)
        {
            await XemBaoCaoAsync();
        }

        private async Task<bool> XemBaoCaoAsync(bool notifyEmpty=true)
        {
            DateTime from=dtpTuNgay.Value,to=dtpDenNgay.Value;
            bool success=false;string caption=btnXem.Text;btnXem.Text="Đang tải…";
            await PosBackground.LoadAsync(this,()=>
            {
                bool ok=bll.LayBaoCao(from,to,out var daily,out var orders,out int count,out decimal total,out decimal discount,out decimal revenue,out string error);
                return (ok,daily,orders,count,total,discount,revenue,error);
            },data=>
            {
                if(!data.ok) {MessageBox.Show(this,data.error,"Báo cáo");return;}
                dgvTheoNgay.DataSource=data.daily;dgvHoaDon.DataSource=data.orders;
                lblSoHD.Text=data.count.ToString("N0",vn);lblTongTien.Text=data.total.ToString("N0",vn)+" đ";
                lblGiamGia.Text=data.discount.ToString("N0",vn)+" đ";lblDoanhThu.Text=data.revenue.ToString("N0",vn)+" đ";
                success=true;
                if(notifyEmpty && data.count==0) MessageBox.Show(this,"Không có hóa đơn trong khoảng ngày này.","Thông báo");
            },error=>MessageBox.Show(this,"Không tải được báo cáo: "+error.Message,"Báo cáo"));
            if(!IsDisposed) btnXem.Text=caption;
            return success;
        }
    }
}
