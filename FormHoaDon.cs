using System;
using System.Linq;
using System.Windows.Forms;
using baitaplon.Models.Services;

namespace baitaplon
{
    public partial class FormHoaDon : Form
    {
        private QuanLyCuaHang _quanLy;
        private DataGridView dgvHoaDon;
        private RichTextBox rtbChiTiet;

        public FormHoaDon(QuanLyCuaHang quanLy)
        {
            _quanLy = quanLy;
            TaoGiaoDien();
            LoadData();
        }

        private void TaoGiaoDien()
        {
            this.Text = "LỊCH SỬ HÓA ĐƠN";
            this.Size = new System.Drawing.Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // Split container
            SplitContainer split = new SplitContainer();
            split.Dock = DockStyle.Fill;
            split.Orientation = Orientation.Vertical;
            split.SplitterDistance = 550;

            // DataGridView hiển thị danh sách hóa đơn
            dgvHoaDon = new DataGridView();
            dgvHoaDon.Dock = DockStyle.Fill;
            dgvHoaDon.BackgroundColor = System.Drawing.Color.White;
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoaDon.SelectionChanged += DgvHoaDon_SelectionChanged;

            // RichTextBox hiển thị chi tiết hóa đơn
            rtbChiTiet = new RichTextBox();
            rtbChiTiet.Dock = DockStyle.Fill;
            rtbChiTiet.Font = new System.Drawing.Font("Consolas", 11);
            rtbChiTiet.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            rtbChiTiet.ForeColor = System.Drawing.Color.White;
            rtbChiTiet.ReadOnly = true;

            split.Panel1.Controls.Add(dgvHoaDon);
            split.Panel2.Controls.Add(rtbChiTiet);

            this.Controls.Add(split);
        }

        private void LoadData()
        {
            var hoaDonList = _quanLy.GetDanhSachHoaDon().Select(hd => new
            {
                hd.MaHoaDon,
                KhachHang = hd.KhachHang == null ? "Khách lẻ" : hd.KhachHang.TenKhach,
                hd.NgayLap,
                SoLuongSach = hd.ChiTiet.Sum(ct => ct.SoLuong),
                hd.TongTien
            }).ToList();

            dgvHoaDon.DataSource = hoaDonList;
        }

        private void DgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                string maHD = dgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value.ToString();
                var hoaDon = _quanLy.GetDanhSachHoaDon().FirstOrDefault(h => h.MaHoaDon == maHD);

                if (hoaDon != null)
                {
                    rtbChiTiet.Clear();
                    rtbChiTiet.AppendText("╔════════════════════════════════════════╗\n");
                    rtbChiTiet.AppendText("║         HÓA ĐƠN BÁN SÁCH              ║\n");
                    rtbChiTiet.AppendText("╠════════════════════════════════════════╣\n");
                    rtbChiTiet.AppendText("║ Mã HD: " + hoaDon.MaHoaDon.PadRight(34) + "║\n");
                    rtbChiTiet.AppendText("║ Ngày: " + hoaDon.NgayLap.ToString("dd/MM/yyyy HH:mm").PadRight(33) + "║\n");
                    rtbChiTiet.AppendText("║ Khách: " + (hoaDon.KhachHang == null ? "Khách lẻ" : hoaDon.KhachHang.TenKhach).PadRight(34) + "║\n");
                    rtbChiTiet.AppendText("╠════════════════════════════════════════╣\n");
                    rtbChiTiet.AppendText("║ CHI TIẾT:                               ║\n");

                    foreach (var ct in hoaDon.ChiTiet)
                    {
                        string tenSach = ct.Sach.TenSach;
                        if (tenSach.Length > 36) tenSach = tenSach.Substring(0, 33) + "...";
                        rtbChiTiet.AppendText("║ • " + tenSach.PadRight(36) + "║\n");
                        rtbChiTiet.AppendText("║   SL: " + ct.SoLuong.ToString().PadRight(3) + " x " + ct.Sach.TinhGiaSauChietKhau().ToString("N0") + " = " + ct.ThanhTien.ToString("N0") + " VNĐ║\n");
                    }

                    rtbChiTiet.AppendText("╠════════════════════════════════════════╣\n");
                    rtbChiTiet.AppendText("║ TỔNG TIỀN: " + hoaDon.TongTien.ToString("N0") + " VNĐ".PadRight(28) + "║\n");
                    rtbChiTiet.AppendText("╚════════════════════════════════════════╝\n");
                }
            }
        }
    }
}
