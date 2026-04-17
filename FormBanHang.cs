using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using baitaplon.Models.Entities;
using baitaplon.Models.Services;

namespace baitaplon
{
    public partial class FormBanHang : Form
    {
        private QuanLyCuaHang _quanLy;
        private ComboBox cboKhachHang, cboSach;
        private NumericUpDown nudSoLuong;
        private DataGridView dvgGioHang;
        private Label lblTongTien;
        private Button btnThemVaoGio, btnXoaKhoiGio, btnThanhToan;
        private List<ChiTietHoaDon> gioHang;

        public FormBanHang(QuanLyCuaHang quanLy)
        {
            _quanLy = quanLy;
            gioHang = new List<ChiTietHoaDon>();
            TaoGiaoDien();
            LoadData();
        }

        private void TaoGiaoDien()
        {
            this.Text = "BÁN HÀNG";
            this.Size = new System.Drawing.Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // Panel thông tin
            Panel pnlInfo = new Panel();
            pnlInfo.Location = new System.Drawing.Point(12, 12);
            pnlInfo.Size = new System.Drawing.Size(1060, 100);
            pnlInfo.BackColor = System.Drawing.Color.White;
            pnlInfo.BorderStyle = BorderStyle.FixedSingle;

            Label lblKhach = new Label();
            lblKhach.Text = "Khách hàng:";
            lblKhach.Location = new System.Drawing.Point(20, 20);
            lblKhach.Size = new System.Drawing.Size(100, 25);
            lblKhach.Font = new System.Drawing.Font("Segoe UI", 10);

            cboKhachHang = new ComboBox();
            cboKhachHang.Location = new System.Drawing.Point(130, 17);
            cboKhachHang.Size = new System.Drawing.Size(200, 30);
            cboKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhachHang.Font = new System.Drawing.Font("Segoe UI", 10);

            Label lblSach = new Label();
            lblSach.Text = "Chọn sách:";
            lblSach.Location = new System.Drawing.Point(380, 20);
            lblSach.Size = new System.Drawing.Size(100, 25);
            lblSach.Font = new System.Drawing.Font("Segoe UI", 10);

            cboSach = new ComboBox();
            cboSach.Location = new System.Drawing.Point(490, 17);
            cboSach.Size = new System.Drawing.Size(350, 30);
            cboSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSach.Font = new System.Drawing.Font("Segoe UI", 10);

            Label lblSL = new Label();
            lblSL.Text = "Số lượng:";
            lblSL.Location = new System.Drawing.Point(20, 60);
            lblSL.Size = new System.Drawing.Size(100, 25);
            lblSL.Font = new System.Drawing.Font("Segoe UI", 10);

            nudSoLuong = new NumericUpDown();
            nudSoLuong.Location = new System.Drawing.Point(130, 57);
            nudSoLuong.Size = new System.Drawing.Size(100, 30);
            nudSoLuong.Minimum = 1;
            nudSoLuong.Maximum = 1000;
            nudSoLuong.Value = 1;
            nudSoLuong.Font = new System.Drawing.Font("Segoe UI", 10);

            btnThemVaoGio = new Button();
            btnThemVaoGio.Text = "Thêm vào giỏ";
            btnThemVaoGio.Location = new System.Drawing.Point(490, 55);
            btnThemVaoGio.Size = new System.Drawing.Size(150, 35);
            btnThemVaoGio.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnThemVaoGio.ForeColor = System.Drawing.Color.White;
            btnThemVaoGio.FlatStyle = FlatStyle.Flat;
            btnThemVaoGio.Cursor = Cursors.Hand;
            btnThemVaoGio.Click += BtnThemVaoGio_Click;

            pnlInfo.Controls.Add(lblKhach);
            pnlInfo.Controls.Add(cboKhachHang);
            pnlInfo.Controls.Add(lblSach);
            pnlInfo.Controls.Add(cboSach);
            pnlInfo.Controls.Add(lblSL);
            pnlInfo.Controls.Add(nudSoLuong);
            pnlInfo.Controls.Add(btnThemVaoGio);

            // DataGridView giỏ hàng
            dvgGioHang = new DataGridView();
            dvgGioHang.Location = new System.Drawing.Point(12, 125);
            dvgGioHang.Size = new System.Drawing.Size(1060, 400);
            dvgGioHang.BackgroundColor = System.Drawing.Color.White;
            dvgGioHang.BorderStyle = BorderStyle.None;
            dvgGioHang.AllowUserToAddRows = false;
            dvgGioHang.AllowUserToDeleteRows = false;
            dvgGioHang.ReadOnly = true;
            dvgGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Panel thanh toán
            Panel pnlThanhToan = new Panel();
            pnlThanhToan.Location = new System.Drawing.Point(12, 540);
            pnlThanhToan.Size = new System.Drawing.Size(1060, 100);
            pnlThanhToan.BackColor = System.Drawing.Color.White;
            pnlThanhToan.BorderStyle = BorderStyle.FixedSingle;

            lblTongTien = new Label();
            lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            lblTongTien.Location = new System.Drawing.Point(20, 30);
            lblTongTien.Size = new System.Drawing.Size(400, 40);
            lblTongTien.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            lblTongTien.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);

            btnXoaKhoiGio = new Button();
            btnXoaKhoiGio.Text = "Xóa khỏi giỏ";
            btnXoaKhoiGio.Location = new System.Drawing.Point(650, 30);
            btnXoaKhoiGio.Size = new System.Drawing.Size(150, 40);
            btnXoaKhoiGio.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            btnXoaKhoiGio.ForeColor = System.Drawing.Color.White;
            btnXoaKhoiGio.FlatStyle = FlatStyle.Flat;
            btnXoaKhoiGio.Cursor = Cursors.Hand;
            btnXoaKhoiGio.Click += BtnXoaKhoiGio_Click;

            btnThanhToan = new Button();
            btnThanhToan.Text = "THANH TOÁN";
            btnThanhToan.Location = new System.Drawing.Point(820, 30);
            btnThanhToan.Size = new System.Drawing.Size(200, 40);
            btnThanhToan.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnThanhToan.ForeColor = System.Drawing.Color.White;
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btnThanhToan.Cursor = Cursors.Hand;
            btnThanhToan.Click += BtnThanhToan_Click;

            pnlThanhToan.Controls.Add(lblTongTien);
            pnlThanhToan.Controls.Add(btnXoaKhoiGio);
            pnlThanhToan.Controls.Add(btnThanhToan);

            this.Controls.Add(pnlInfo);
            this.Controls.Add(dvgGioHang);
            this.Controls.Add(pnlThanhToan);
        }

        private void LoadData()
        {
            var khachList = _quanLy.GetDanhSachKhach();
            cboKhachHang.DataSource = khachList;
            cboKhachHang.DisplayMember = "TenKhach";
            cboKhachHang.ValueMember = "MaKhach";

            var sachList = _quanLy.GetDanhSachSach();
            var sachData = new List<object>();
            foreach (var s in sachList)
            {
                if (s.SoLuongTon > 0)
                {
                    sachData.Add(new
                    {
                        s.MaSach,
                        DisplayText = s.MaSach + " - " + s.TenSach + " - " + s.GiaBan.ToString("N0") + " VNĐ (Còn: " + s.SoLuongTon + ")",
                        s.TenSach,
                        s.GiaBan,
                        s.SoLuongTon
                    });
                }
            }
            cboSach.DataSource = sachData;
            cboSach.DisplayMember = "DisplayText";
            cboSach.ValueMember = "MaSach";

            UpdateGioHangDisplay();
        }

        private void UpdateGioHangDisplay()
        {
            var data = new List<object>();
            foreach (var ct in gioHang)
            {
                data.Add(new
                {
                    ct.Sach.MaSach,
                    ct.Sach.TenSach,
                    ct.Sach.GiaBan,
                    GiaSauChietKhau = ct.Sach.TinhGiaSauChietKhau(),
                    ct.SoLuong,
                    ct.ThanhTien
                });
            }

            dvgGioHang.DataSource = null;
            dvgGioHang.DataSource = data;

            double tong = 0;
            foreach (var ct in gioHang)
                tong += ct.ThanhTien;
            lblTongTien.Text = "Tổng tiền: " + tong.ToString("N0") + " VNĐ";
        }

        private void BtnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (cboSach.SelectedItem == null) return;

            dynamic selectedSach = cboSach.SelectedItem;
            string maSach = selectedSach.MaSach;
            var sach = _quanLy.TimSachTheoMa(maSach);
            int soLuong = (int)nudSoLuong.Value;

            if (soLuong > sach.SoLuongTon)
            {
                MessageBox.Show("Số lượng không đủ! Chỉ còn " + sach.SoLuongTon + " cuốn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existing = gioHang.FirstOrDefault(ct => ct.Sach.MaSach == maSach);
            if (existing != null)
                existing.SoLuong += soLuong;
            else
                gioHang.Add(new ChiTietHoaDon(sach, soLuong));

            UpdateGioHangDisplay();
            MessageBox.Show("Đã thêm " + soLuong + " cuốn " + sach.TenSach + " vào giỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnXoaKhoiGio_Click(object sender, EventArgs e)
        {
            if (dvgGioHang.SelectedRows.Count > 0)
            {
                string maSach = dvgGioHang.SelectedRows[0].Cells["MaSach"].Value.ToString();
                var item = gioHang.FirstOrDefault(ct => ct.Sach.MaSach == maSach);
                if (item != null)
                {
                    gioHang.Remove(item);
                    UpdateGioHangDisplay();
                    MessageBox.Show("Đã xóa khỏi giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            if (gioHang.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var khach = (KhachHang)cboKhachHang.SelectedItem;
            var dictSach = new Dictionary<string, int>();
            foreach (var ct in gioHang)
                dictSach.Add(ct.Sach.MaSach, ct.SoLuong);

            var hoaDon = _quanLy.TaoHoaDon(khach.MaKhach, dictSach);

            MessageBox.Show("THANH TOÁN THÀNH CÔNG!\n\nMã hóa đơn: " + hoaDon.MaHoaDon + "\nKhách hàng: " + khach.TenKhach + "\nTổng tiền: " + hoaDon.TongTien.ToString("N0") + " VNĐ\nNgày: " + hoaDon.NgayLap.ToString("dd/MM/yyyy HH:mm"), "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            gioHang.Clear();
            UpdateGioHangDisplay();
            LoadData();
            this.Close();
        }
    }
}