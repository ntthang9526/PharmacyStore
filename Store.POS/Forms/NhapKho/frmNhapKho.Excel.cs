using System.Data;
namespace Store.POS;
public partial class frmNhapKho
{
        private void TaiFileMauNhapKho()
        {
            if (cboNhaCungCap.SelectedValue is not int maNCC || maNCC <= 0)
            {
                MessageBox.Show("Chọn nhà cung cấp trước khi tải file mẫu.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var dtThuocNCC = bll.LayThuocTheoNCC(maNCC);
                string tenNCC = cboNhaCungCap.Text;
                Store.POS.Common.ExcelHelper.XuatFileMauNhapKho(tenNCC, dtThuocNCC);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NhapExcelPhieuNhap()
        {
            if (cboNhaCungCap.SelectedValue is not int maNCC || maNCC <= 0)
            {
                MessageBox.Show("Chọn nhà cung cấp trước khi nhập Excel.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }

            string? duongDan = Store.POS.Common.ExcelHelper.ChonFileExcelDeDoc();
            if (string.IsNullOrEmpty(duongDan)) return;

            try
            {
                var dtThuocNCC = bll.LayThuocTheoNCC(maNCC);
                if (dtThuocNCC.Rows.Count == 0)
                {
                    MessageBox.Show("Nhà cung cấp chưa có thuốc. Thêm thuốc trước khi nhập Excel.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var danhSach = Store.POS.Common.ExcelHelper.DocDanhSachNhapKhoTuExcel(duongDan, dtThuocNCC, out var errors);
                if (danhSach.Count == 0)
                {
                    string msg = "File Excel chưa có dòng hợp lệ để nhập.";
                    if (errors.Count > 0)
                        msg += "\n\nChi tiết lỗi:\n" + string.Join("\n", errors.Take(8));
                    MessageBox.Show(msg, "Lỗi nhập file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Map để lấy Đơn vị tính của từng thuốc
                var dicDonVi = new Dictionary<int, string>();
                foreach (DataRow r in dtThuocNCC.Rows)
                    dicDonVi[Convert.ToInt32(r["MaThuoc"])] = r["DonViTinh"]?.ToString() ?? "";

                var preview=new DataTable();
                foreach(string name in new[]{"Thuốc","Số lô","Hạn dùng","Số lượng","Giá nhập"}) preview.Columns.Add(name);
                danhSach=danhSach.Where(row=>
                {
                    if(row.SoLo.Length<=50 && row.GiaNhap<=9999999999999999.99m) return true;
                    errors.Add($"Dòng {row.RowIndex}: Số lô hoặc giá nhập vượt giới hạn.");return false;
                }).ToList();
                foreach(var row in danhSach) preview.Rows.Add(row.TenThuoc,row.SoLo,row.HanDung.ToString("dd/MM/yyyy"),row.SoLuong,Store.Shared.TienTe.LamTron(row.GiaNhap).ToString("N0"));
                using(var dialog=new frmXemTruocExcel(preview,errors)) if(dialog.ShowDialog(this)!=DialogResult.OK) return;
                int themThanhCong = 0;
                foreach (var item in danhSach)
                {
                    string dvt = dicDonVi.TryGetValue(item.MaThuoc, out var dv) ? dv : "";
                    dtPhieu.Rows.Add(
                        item.MaThuoc,
                        item.TenThuoc,
                        dvt,
                        item.SoLo,
                        item.HanDung,
                        item.SoLuong,
                        Store.Shared.TienTe.LamTron(item.GiaNhap));
                    themThanhCong++;
                }

                CapNhatTongVon();

                string ketQua = $"Đã thêm {themThanhCong} dòng vào phiếu nhập.";
                if (errors.Count > 0)
                {
                    ketQua += $"\n\nBỏ qua {errors.Count} dòng có lỗi:\n" + string.Join("\n", errors.Take(6));
                    if (errors.Count > 6) ketQua += $"\n... và {errors.Count - 6} lỗi khác.";
                    MessageBox.Show(ketQua, "Kết quả nhập Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ketQua + "\nKiểm tra danh sách rồi bấm Lưu phiếu.",
                        "Nhập Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

}
