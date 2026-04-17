using System;
using System.Windows.Forms;
using baitaplon.Models.Entities;
using baitaplon.Models.Services;

namespace baitaplon
{
    public partial class FormThemSach : Form
    {
        private QuanLyCuaHang _quanLy;
        private ComboBox cboLoaiSach;
        private TextBox txtMaSach, txtTenSach, txtTacGia, txtNamXB, txtGia, txtSoLuong, txtThemInfo;
        private CheckBox chkBanMau;
        private Button btnSave, btnCancel;

        public FormThemSach(QuanLyCuaHang quanLy)
        {
            _quanLy = quanLy;
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            this.Text = "THÊM SÁCH MỚI";
            this.Size = new System.Drawing.Size(500, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.Padding = new Padding(20);
            tlp.ColumnCount = 2;
            tlp.RowCount = 11;
            for (int i = 0; i < 11; i++)
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            // Loại sách
            Label lblLoai = new Label();
            lblLoai.Text = "Loại sách:";
            lblLoai.Font = new System.Drawing.Font("Segoe UI", 10);
            lblLoai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            tlp.Controls.Add(lblLoai, 0, 0);

            cboLoaiSach = new ComboBox();
            cboLoaiSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiSach.Font = new System.Drawing.Font("Segoe UI", 10);
            cboLoaiSach.Height = 30;
            cboLoaiSach.Items.AddRange(new[] { "Sách Giáo Khoa", "Sách Tham Khảo", "Sách Văn Học" });
            cboLoaiSach.SelectedIndex = 0;
            cboLoaiSach.SelectedIndexChanged += CboLoaiSach_SelectedIndexChanged;
            tlp.Controls.Add(cboLoaiSach, 1, 0);

            // Mã sách
            Label lblMa = new Label();
            lblMa.Text = "Mã sách (để trống tự tạo):";
            lblMa.Font = new System.Drawing.Font("Segoe UI", 10);
            lblMa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            tlp.Controls.Add(lblMa, 0, 1);

            txtMaSach = new TextBox();
            txtMaSach.Font = new System.Drawing.Font("Segoe UI", 10);
            txtMaSach.Height = 30;
            tlp.Controls.Add(txtMaSach, 1, 1);

            // Tên sách
            Label lblTen = new Label();
            lblTen.Text = "Tên sách:";
            lblTen.Font = new System.Drawing.Font("Segoe UI", 10);
            lblTen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            tlp.Controls.Add(lblTen, 0, 2);

            txtTenSach = new TextBox();
            txtTenSach.Font = new System.Drawing.Font("Segoe UI", 10);
            txtTenSach.Height = 30;
            tlp.Controls.Add(txtTenSach, 1, 2);

            // Tác giả
            Label lblTacGia = new Label();
            lblTacGia.Text = "Tác giả:";
            lblTacGia.Font = new System.Drawing.Font("Segoe UI", 10);
            lblTacGia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            tlp.Controls.Add(lblTacGia, 0, 3);

            txtTacGia = new TextBox();
            txtTacGia.Font = new System.Drawing.Font("Segoe UI", 10);
            txtTacGia.Height = 30;
            tlp.Controls.Add(txtTacGia, 1, 3);

            // Năm XB
            Label lblNam = new Label();
            lblNam.Text = "Năm xuất bản:";
            lblNam.Font = new System.Drawing.Font("Segoe UI", 10);
            lblNam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            tlp.Controls.Add(lblNam, 0, 4);

            txtNamXB = new TextBox();
            txtNamXB.Text = "2024";
            txtNamXB.Font = new System.Drawing.Font("Segoe UI", 10);
            txtNamXB.Height = 30;
            tlp.Controls.Add(txtNamXB, 1, 4);

            // Giá bán
            Label lblGia = new Label();
            lblGia.Text = "Giá bán:";
            lblGia.Font = new System.Drawing.Font("Segoe UI", 10);
            lblGia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            tlp.Controls.Add(lblGia, 0, 5);

            txtGia = new TextBox();
            txtGia.Font = new System.Drawing.Font("Segoe UI", 10);
            txtGia.Height = 30;
            tlp.Controls.Add(txtGia, 1, 5);

            // Số lượng
            Label lblSL = new Label();
            lblSL.Text = "Số lượng tồn:";
            lblSL.Font = new System.Drawing.Font("Segoe UI", 10);
            lblSL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            tlp.Controls.Add(lblSL, 0, 6);

            txtSoLuong = new TextBox();
            txtSoLuong.Text = "0";
            txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 10);
            txtSoLuong.Height = 30;
            tlp.Controls.Add(txtSoLuong, 1, 6);

            // Panel thông tin bổ sung
            Panel extraPanel = new Panel();
            extraPanel.Height = 80;

            txtThemInfo = new TextBox();
            txtThemInfo.Font = new System.Drawing.Font("Segoe UI", 10);
            txtThemInfo.Height = 30;
            txtThemInfo.Width = 280;
            txtThemInfo.Location = new System.Drawing.Point(0, 0);

            chkBanMau = new CheckBox();
            chkBanMau.Text = "Bản màu (giảm 10%)";
            chkBanMau.Font = new System.Drawing.Font("Segoe UI", 10);
            chkBanMau.Location = new System.Drawing.Point(0, 45);
            chkBanMau.Visible = false;

            extraPanel.Controls.Add(txtThemInfo);
            extraPanel.Controls.Add(chkBanMau);

            Label lblThem = new Label();
            lblThem.Text = "Thông tin bổ sung:";
            lblThem.Font = new System.Drawing.Font("Segoe UI", 10);
            lblThem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            tlp.Controls.Add(lblThem, 0, 7);

            tlp.Controls.Add(extraPanel, 1, 7);
            tlp.SetRowSpan(extraPanel, 2);

            // Buttons
            btnSave = new Button();
            btnSave.Text = "LƯU SÁCH";
            btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Height = 40;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            btnSave.Cursor = Cursors.Hand;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button();
            btnCancel.Text = "HỦY";
            btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Height = 40;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += (s, e) => this.Close();

            tlp.Controls.Add(btnSave, 0, 9);
            tlp.Controls.Add(btnCancel, 1, 9);

            this.Controls.Add(tlp);
        }

        private void CboLoaiSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = cboLoaiSach.SelectedItem.ToString();
            if (loai == "Sách Giáo Khoa")
            {
                txtThemInfo.Visible = true;
                txtThemInfo.Text = "";
                chkBanMau.Visible = true;
            }
            else if (loai == "Sách Tham Khảo")
            {
                txtThemInfo.Visible = true;
                txtThemInfo.Text = "";
                chkBanMau.Visible = false;
            }
            else
            {
                txtThemInfo.Visible = true;
                txtThemInfo.Text = "";
                chkBanMau.Visible = false;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double gia;
                if (string.IsNullOrWhiteSpace(txtGia.Text) || !double.TryParse(txtGia.Text, out gia) || gia <= 0)
                {
                    MessageBox.Show("Vui lòng nhập giá bán hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int namXB = 2024;
                if (!string.IsNullOrWhiteSpace(txtNamXB.Text))
                    int.TryParse(txtNamXB.Text, out namXB);

                int soLuong = 0;
                if (!string.IsNullOrWhiteSpace(txtSoLuong.Text))
                    int.TryParse(txtSoLuong.Text, out soLuong);

                string loai = cboLoaiSach.SelectedItem.ToString();
                Sach sach = null;

                if (loai == "Sách Giáo Khoa")
                {
                    SachGiaoKhoa sgk = new SachGiaoKhoa();
                    sgk.MaSach = txtMaSach.Text;
                    sgk.TenSach = txtTenSach.Text;
                    sgk.TacGia = txtTacGia.Text;
                    sgk.NamXuatBan = namXB;
                    sgk.GiaBan = gia;
                    sgk.SoLuongTon = soLuong;
                    sgk.Lop = txtThemInfo.Text;
                    sgk.CoBanMau = chkBanMau.Checked;
                    sach = sgk;
                }
                else if (loai == "Sách Tham Khảo")
                {
                    SachThamKhao stk = new SachThamKhao();
                    stk.MaSach = txtMaSach.Text;
                    stk.TenSach = txtTenSach.Text;
                    stk.TacGia = txtTacGia.Text;
                    stk.NamXuatBan = namXB;
                    stk.GiaBan = gia;
                    stk.SoLuongTon = soLuong;
                    stk.LinhVuc = txtThemInfo.Text;
                    sach = stk;
                }
                else
                {
                    SachVanHoc svh = new SachVanHoc();
                    svh.MaSach = txtMaSach.Text;
                    svh.TenSach = txtTenSach.Text;
                    svh.TacGia = txtTacGia.Text;
                    svh.NamXuatBan = namXB;
                    svh.GiaBan = gia;
                    svh.SoLuongTon = soLuong;
                    svh.TheLoai = txtThemInfo.Text;
                    sach = svh;
                }

                _quanLy.ThemSach(sach);
                MessageBox.Show("Thêm sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}