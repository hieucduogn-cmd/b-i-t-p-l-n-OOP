using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using baitaplon.Models.Entities;
using baitaplon.Models.Services;
using System.Drawing;

namespace baitaplon
{
    public partial class FormQuanLy : Form
    {
        private QuanLyCuaHang _quanLy;
        private DataGridView dgvSach;
        private Label lblTongSach, lblTongDoanhThu, lblTongHoaDon;
        private Button btnThemSach, btnXoaSach, btnBanHang, btnXemHoaDon;
        private TextBox txtTimKiem;
        private ComboBox cboLoaiTim;

        public FormQuanLy()
        {
            _quanLy = new QuanLyCuaHang();
            TaoGiaoDien();
            LoadData();
            CapNhatThongKe();
        }

        // Hàm tạo nút bo tròn
        private Button TaoNutBoTron(string text, int x, int y, int width, int height, Color mauNen, Color mauHover)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(width, height);
            btn.BackColor = mauNen;
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;

            GraphicsPath path = new GraphicsPath();
            int borderRadius = 20;
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(btn.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(btn.Width - borderRadius, btn.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, btn.Height - borderRadius, borderRadius, borderRadius, 90, 90);
            btn.Region = new Region(path);

            btn.MouseEnter += (s, e) => { btn.BackColor = mauHover; };
            btn.MouseLeave += (s, e) => { btn.BackColor = mauNen; };

            return btn;
        }

        private void TaoGiaoDien()
        {
            this.Text = "📚 QUẢN LÝ CỬA HÀNG SÁCH";
            this.Size = new System.Drawing.Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 11);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ==================== HEADER ====================
            Panel headerPanel = new Panel();
            headerPanel.Location = new System.Drawing.Point(0, 0);
            headerPanel.Size = new System.Drawing.Size(1400, 80);
            headerPanel.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);

            Label lblTitle = new Label();
            lblTitle.Text = "📚 QUẢN LÝ CỬA HÀNG SÁCH";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 24, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(30, 22);
            lblTitle.AutoSize = true;
            headerPanel.Controls.Add(lblTitle);

            // ==================== PANEL NÚT ====================
            Panel btnPanel = new Panel();
            btnPanel.Location = new System.Drawing.Point(12, 95);
            btnPanel.Size = new System.Drawing.Size(1360, 55);
            btnPanel.BackColor = System.Drawing.Color.White;

            btnThemSach = TaoNutBoTron("➕ Thêm sách", 15, 8, 140, 40,
                System.Drawing.Color.FromArgb(46, 204, 113),
                System.Drawing.Color.FromArgb(39, 174, 96));
            btnThemSach.Click += BtnThemSach_Click;

            btnXoaSach = TaoNutBoTron("🗑️ Xóa sách", 170, 8, 140, 40,
                System.Drawing.Color.FromArgb(231, 76, 60),
                System.Drawing.Color.FromArgb(192, 57, 43));
            btnXoaSach.Click += BtnXoaSach_Click;

            btnBanHang = TaoNutBoTron("🛒 Bán hàng", 325, 8, 140, 40,
                System.Drawing.Color.FromArgb(52, 152, 219),
                System.Drawing.Color.FromArgb(41, 128, 185));
            btnBanHang.Click += BtnBanHang_Click;

            btnXemHoaDon = TaoNutBoTron("📄 Hóa đơn", 480, 8, 140, 40,
                System.Drawing.Color.FromArgb(155, 89, 182),
                System.Drawing.Color.FromArgb(142, 68, 173));
            btnXemHoaDon.Click += BtnXemHoaDon_Click;

            btnPanel.Controls.Add(btnThemSach);
            btnPanel.Controls.Add(btnXoaSach);
            btnPanel.Controls.Add(btnBanHang);
            btnPanel.Controls.Add(btnXemHoaDon);

            // ==================== PANEL TÌM KIẾM + ĐỒNG HỒ ====================
            Panel searchPanel = new Panel();
            searchPanel.Location = new System.Drawing.Point(12, 160);
            searchPanel.Size = new System.Drawing.Size(1360, 50);
            searchPanel.BackColor = System.Drawing.Color.White;

            Label lblTim = new Label();
            lblTim.Text = "🔍 Tìm kiếm:";
            lblTim.Font = new System.Drawing.Font("Segoe UI", 11);
            lblTim.Location = new System.Drawing.Point(680, 12);
            lblTim.Size = new System.Drawing.Size(80, 25);
            lblTim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtTimKiem = new TextBox();
            txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11);
            txtTimKiem.Location = new System.Drawing.Point(770, 10);
            txtTimKiem.Size = new System.Drawing.Size(180, 30);

            cboLoaiTim = new ComboBox();
            cboLoaiTim.Font = new System.Drawing.Font("Segoe UI", 11);
            cboLoaiTim.Location = new System.Drawing.Point(960, 10);
            cboLoaiTim.Size = new System.Drawing.Size(100, 28);
            cboLoaiTim.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiTim.Items.AddRange(new object[] { "Theo mã", "Theo tên" });
            cboLoaiTim.SelectedIndex = 0;

            Button btnTim = new Button();
            btnTim.Text = "Tìm";
            btnTim.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnTim.Location = new System.Drawing.Point(1070, 9);
            btnTim.Size = new System.Drawing.Size(70, 32);
            btnTim.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnTim.ForeColor = System.Drawing.Color.White;
            btnTim.FlatStyle = FlatStyle.Flat;
            btnTim.Cursor = Cursors.Hand;
            btnTim.Click += BtnTimKiem_Click;

            // Đồng hồ mini
            Panel clockMini = new Panel();
            clockMini.Location = new System.Drawing.Point(1155, 5);
            clockMini.Size = new System.Drawing.Size(190, 42);
            clockMini.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);

            Label lblClockIcon = new Label();
            lblClockIcon.Text = "🕐";
            lblClockIcon.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            lblClockIcon.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15);
            lblClockIcon.Location = new System.Drawing.Point(8, 8);
            lblClockIcon.AutoSize = true;
            clockMini.Controls.Add(lblClockIcon);

            Label lblDateTime = new Label();
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy\nHH:mm:ss");
            lblDateTime.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            lblDateTime.ForeColor = System.Drawing.Color.White;
            lblDateTime.Location = new System.Drawing.Point(40, 5);
            lblDateTime.Size = new System.Drawing.Size(140, 35);
            lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            clockMini.Controls.Add(lblDateTime);

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy\nHH:mm:ss");
            };
            timer.Start();

            searchPanel.Controls.Add(lblTim);
            searchPanel.Controls.Add(txtTimKiem);
            searchPanel.Controls.Add(cboLoaiTim);
            searchPanel.Controls.Add(btnTim);
            searchPanel.Controls.Add(clockMini);

            // ==================== PANEL THỐNG KÊ ====================
            Panel statsPanel = new Panel();
            statsPanel.Location = new System.Drawing.Point(12, 220);
            statsPanel.Size = new System.Drawing.Size(1360, 45);
            statsPanel.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);

            lblTongSach = new Label();
            lblTongSach.Text = "📖 Tổng sách: 0";
            lblTongSach.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            lblTongSach.ForeColor = System.Drawing.Color.White;
            lblTongSach.Location = new System.Drawing.Point(20, 12);
            lblTongSach.AutoSize = true;

            lblTongDoanhThu = new Label();
            lblTongDoanhThu.Text = "💰 Doanh thu: 0 VNĐ";
            lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            lblTongDoanhThu.ForeColor = System.Drawing.Color.White;
            lblTongDoanhThu.Location = new System.Drawing.Point(450, 12);
            lblTongDoanhThu.AutoSize = true;

            lblTongHoaDon = new Label();
            lblTongHoaDon.Text = "🧾 Hóa đơn: 0";
            lblTongHoaDon.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            lblTongHoaDon.ForeColor = System.Drawing.Color.White;
            lblTongHoaDon.Location = new System.Drawing.Point(950, 12);
            lblTongHoaDon.AutoSize = true;

            statsPanel.Controls.Add(lblTongSach);
            statsPanel.Controls.Add(lblTongDoanhThu);
            statsPanel.Controls.Add(lblTongHoaDon);

            // ==================== DATAGRIDVIEW ====================
            dgvSach = new DataGridView();
            dgvSach.Location = new System.Drawing.Point(12, 275);
            dgvSach.Size = new System.Drawing.Size(1360, 480);
            dgvSach.AllowUserToAddRows = false;
            dgvSach.ReadOnly = true;
            dgvSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSach.BackgroundColor = System.Drawing.Color.White;
            dgvSach.BorderStyle = BorderStyle.None;
            dgvSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSach.Font = new System.Drawing.Font("Segoe UI", 11);
            dgvSach.RowTemplate.Height = 40;
            dgvSach.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            dgvSach.RowHeadersVisible = false;
            dgvSach.GridColor = System.Drawing.Color.FromArgb(200, 200, 200);
            dgvSach.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvSach.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            dgvSach.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvSach.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            dgvSach.EnableHeadersVisualStyles = false;

            this.Controls.Add(headerPanel);
            this.Controls.Add(btnPanel);
            this.Controls.Add(searchPanel);
            this.Controls.Add(statsPanel);
            this.Controls.Add(dgvSach);
        }

        private void LoadData(string searchTerm = "", string searchType = "Theo mã")
        {
            var sachList = _quanLy.GetDanhSachSach();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (searchType == "Theo mã")
                    sachList = sachList.Where(s => s.MaSach.ToLower().Contains(searchTerm.ToLower())).ToList();
                else
                    sachList = sachList.Where(s => s.TenSach.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            var data = sachList.Select(s => new
            {
                s.MaSach,
                s.TenSach,
                s.TacGia,
                s.NamXuatBan,
                GiaBan = s.GiaBan,
                s.SoLuongTon,
                LoaiSach = s.GetLoaiSach(),
                GiaSauCK = s.TinhGiaSauChietKhau()
            }).ToList();

            dgvSach.DataSource = null;
            dgvSach.DataSource = data;

            if (dgvSach.Columns.Contains("GiaBan"))
                dgvSach.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            if (dgvSach.Columns.Contains("GiaSauCK"))
                dgvSach.Columns["GiaSauCK"].DefaultCellStyle.Format = "N0";
        }

        private void CapNhatThongKe()
        {
            lblTongSach.Text = $"📖 Tổng sách: {_quanLy.GetTongSoLuongSach()}";
            lblTongDoanhThu.Text = $"💰 Doanh thu: {_quanLy.GetTongDoanhThu():N0} VNĐ";
            lblTongHoaDon.Text = $"🧾 Hóa đơn: {_quanLy.GetTongSoHoaDon()}";
        }

        private void BtnThemSach_Click(object sender, EventArgs e)
        {
            FormThemSach formThem = new FormThemSach(_quanLy);
            formThem.ShowDialog();
            LoadData();
            CapNhatThongKe();
        }

        private void BtnXoaSach_Click(object sender, EventArgs e)
        {
            if (dgvSach.SelectedRows.Count > 0)
            {
                string maSach = dgvSach.SelectedRows[0].Cells["MaSach"].Value.ToString();
                string tenSach = dgvSach.SelectedRows[0].Cells["TenSach"].Value.ToString();
                if (MessageBox.Show($"Xóa sách \"{tenSach}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _quanLy.XoaSach(maSach);
                    LoadData();
                    CapNhatThongKe();
                    MessageBox.Show("Đã xóa sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnBanHang_Click(object sender, EventArgs e)
        {
            FormBanHang formBanHang = new FormBanHang(_quanLy);
            formBanHang.ShowDialog();
            LoadData();
            CapNhatThongKe();
        }

        private void BtnXemHoaDon_Click(object sender, EventArgs e)
        {
            FormHoaDon formHoaDon = new FormHoaDon(_quanLy);
            formHoaDon.ShowDialog();
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text, cboLoaiTim.SelectedItem.ToString());
        }
    }
}