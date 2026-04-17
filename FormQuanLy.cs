using System;
using System.Linq;
using System.Windows.Forms;
using baitaplon.Models.Entities;
using baitaplon.Models.Services;

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

        private void TaoGiaoDien()
        {
            this.Text = "📚 QUẢN LÝ CỬA HÀNG SÁCH";
            this.Size = new System.Drawing.Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 11);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Header
            Panel headerPanel = new Panel();
            headerPanel.Location = new System.Drawing.Point(0, 0);
            headerPanel.Size = new System.Drawing.Size(1400, 90);
            headerPanel.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);

            Label lblTitle = new Label();
            lblTitle.Text = "📚 QUẢN LÝ CỬA HÀNG SÁCH";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 26, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(40, 25);
            lblTitle.AutoSize = true;
            headerPanel.Controls.Add(lblTitle);

            // Panel nút
            Panel btnPanel = new Panel();
            btnPanel.Location = new System.Drawing.Point(15, 105);
            btnPanel.Size = new System.Drawing.Size(1360, 60);
            btnPanel.BackColor = System.Drawing.Color.White;

            btnThemSach = new Button();
            btnThemSach.Text = "➕ Thêm sách";
            btnThemSach.Location = new System.Drawing.Point(15, 10);
            btnThemSach.Size = new System.Drawing.Size(150, 45);
            btnThemSach.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnThemSach.ForeColor = System.Drawing.Color.White;
            btnThemSach.FlatStyle = FlatStyle.Flat;
            btnThemSach.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btnThemSach.Cursor = Cursors.Hand;
            btnThemSach.Click += BtnThemSach_Click;

            btnXoaSach = new Button();
            btnXoaSach.Text = "🗑️ Xóa sách";
            btnXoaSach.Location = new System.Drawing.Point(180, 10);
            btnXoaSach.Size = new System.Drawing.Size(150, 45);
            btnXoaSach.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            btnXoaSach.ForeColor = System.Drawing.Color.White;
            btnXoaSach.FlatStyle = FlatStyle.Flat;
            btnXoaSach.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btnXoaSach.Cursor = Cursors.Hand;
            btnXoaSach.Click += BtnXoaSach_Click;

            btnBanHang = new Button();
            btnBanHang.Text = "🛒 Bán hàng";
            btnBanHang.Location = new System.Drawing.Point(345, 10);
            btnBanHang.Size = new System.Drawing.Size(150, 45);
            btnBanHang.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnBanHang.ForeColor = System.Drawing.Color.White;
            btnBanHang.FlatStyle = FlatStyle.Flat;
            btnBanHang.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btnBanHang.Cursor = Cursors.Hand;
            btnBanHang.Click += BtnBanHang_Click;

            btnXemHoaDon = new Button();
            btnXemHoaDon.Text = "📄 Xem hóa đơn";
            btnXemHoaDon.Location = new System.Drawing.Point(510, 10);
            btnXemHoaDon.Size = new System.Drawing.Size(150, 45);
            btnXemHoaDon.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            btnXemHoaDon.ForeColor = System.Drawing.Color.White;
            btnXemHoaDon.FlatStyle = FlatStyle.Flat;
            btnXemHoaDon.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btnXemHoaDon.Cursor = Cursors.Hand;
            btnXemHoaDon.Click += BtnXemHoaDon_Click;

            btnPanel.Controls.Add(btnThemSach);
            btnPanel.Controls.Add(btnXoaSach);
            btnPanel.Controls.Add(btnBanHang);
            btnPanel.Controls.Add(btnXemHoaDon);

            // Tìm kiếm
            Panel searchPanel = new Panel();
            searchPanel.Location = new System.Drawing.Point(15, 175);
            searchPanel.Size = new System.Drawing.Size(1360, 50);
            searchPanel.BackColor = System.Drawing.Color.White;

            Label lblTim = new Label();
            lblTim.Text = "🔍 Tìm kiếm:";
            lblTim.Font = new System.Drawing.Font("Segoe UI", 12);
            lblTim.Location = new System.Drawing.Point(750, 12);
            lblTim.Size = new System.Drawing.Size(100, 30);
            lblTim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            txtTimKiem = new TextBox();
            txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 12);
            txtTimKiem.Location = new System.Drawing.Point(860, 10);
            txtTimKiem.Size = new System.Drawing.Size(250, 35);

            cboLoaiTim = new ComboBox();
            cboLoaiTim.Font = new System.Drawing.Font("Segoe UI", 12);
            cboLoaiTim.Location = new System.Drawing.Point(1120, 10);
            cboLoaiTim.Size = new System.Drawing.Size(120, 32);
            cboLoaiTim.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiTim.Items.AddRange(new object[] { "Theo mã", "Theo tên" });
            cboLoaiTim.SelectedIndex = 0;

            Button btnTim = new Button();
            btnTim.Text = "Tìm";
            btnTim.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            btnTim.Location = new System.Drawing.Point(1250, 9);
            btnTim.Size = new System.Drawing.Size(100, 35);
            btnTim.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnTim.ForeColor = System.Drawing.Color.White;
            btnTim.FlatStyle = FlatStyle.Flat;
            btnTim.Cursor = Cursors.Hand;
            btnTim.Click += BtnTimKiem_Click;

            searchPanel.Controls.Add(lblTim);
            searchPanel.Controls.Add(txtTimKiem);
            searchPanel.Controls.Add(cboLoaiTim);
            searchPanel.Controls.Add(btnTim);

            // Thống kê
            Panel statsPanel = new Panel();
            statsPanel.Location = new System.Drawing.Point(15, 235);
            statsPanel.Size = new System.Drawing.Size(1360, 55);
            statsPanel.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);

            lblTongSach = new Label();
            lblTongSach.Text = "📖 Tổng sách: 0";
            lblTongSach.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            lblTongSach.ForeColor = System.Drawing.Color.White;
            lblTongSach.Location = new System.Drawing.Point(30, 15);
            lblTongSach.AutoSize = true;

            lblTongDoanhThu = new Label();
            lblTongDoanhThu.Text = "💰 Doanh thu: 0 VNĐ";
            lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            lblTongDoanhThu.ForeColor = System.Drawing.Color.White;
            lblTongDoanhThu.Location = new System.Drawing.Point(500, 15);
            lblTongDoanhThu.AutoSize = true;

            lblTongHoaDon = new Label();
            lblTongHoaDon.Text = "🧾 Hóa đơn: 0";
            lblTongHoaDon.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            lblTongHoaDon.ForeColor = System.Drawing.Color.White;
            lblTongHoaDon.Location = new System.Drawing.Point(1000, 15);
            lblTongHoaDon.AutoSize = true;

            statsPanel.Controls.Add(lblTongSach);
            statsPanel.Controls.Add(lblTongDoanhThu);
            statsPanel.Controls.Add(lblTongHoaDon);

            // DataGridView
            dgvSach = new DataGridView();
            dgvSach.Location = new System.Drawing.Point(15, 300);
            dgvSach.Size = new System.Drawing.Size(1360, 440);
            dgvSach.AllowUserToAddRows = false;
            dgvSach.ReadOnly = true;
            dgvSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSach.BackgroundColor = System.Drawing.Color.White;
            dgvSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSach.Font = new System.Drawing.Font("Segoe UI", 11);
            dgvSach.RowTemplate.Height = 40;
            dgvSach.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            dgvSach.RowHeadersVisible = false;

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
                if (MessageBox.Show($"Xóa sách \"{tenSach}\"?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _quanLy.XoaSach(maSach);
                    LoadData();
                    CapNhatThongKe();
                }
            }
            else
            {
                MessageBox.Show("Chọn sách cần xóa!");
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
