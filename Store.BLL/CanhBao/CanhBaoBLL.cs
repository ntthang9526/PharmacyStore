using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public class CanhBaoBLL
    {
        private CanhBaoDAL dal = new CanhBaoDAL();

        // Thuốc có tồn kho < nguongTon (mặc định 10)
        public DataTable LayThuocSapHet(int nguongTon = 10)
        {
            return dal.LayThuocSapHet(nguongTon);
        }

        // Chỉ lô còn hạn dưới soNgay ngày (mặc định 30), không gồm lô đã hết hạn.
        public DataTable LayLoSapHetHan(int soNgay = 30, bool baoGomHetHan = false)
        {
            return dal.LayLoSapHetHan(soNgay, baoGomHetHan);
        }
    }
}
