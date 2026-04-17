using System;
using System.Collections.Generic;
using System.Linq;
using baitaplon.Models.Entities;

namespace baitaplon.Models.Services
{
    public class QuanLyCuaHang
    {
        private List<Sach> _danhSachSach;
        private List<KhachHang> _danhSachKhach;
        private List<HoaDon> _danhSachHoaDon;

        public QuanLyCuaHang()
        {
            _danhSachSach = new List<Sach>();
            _danhSachKhach = new List<KhachHang>();
            _danhSachHoaDon = new List<HoaDon>();
            TaoDuLieuMau();
        }

        private void TaoDuLieuMau()
        {
            // Thêm sách mẫu
            SachGiaoKhoa sgk1 = new SachGiaoKhoa();
            sgk1.MaSach = "SGK001";
            sgk1.TenSach = "Toán 10";
            sgk1.TacGia = "Nguyễn Văn A";
            sgk1.NamXuatBan = 2023;
            sgk1.GiaBan = 45000;
            sgk1.SoLuongTon = 50;
            sgk1.Lop = "10";
            sgk1.CoBanMau = false;
            _danhSachSach.Add(sgk1);

            SachGiaoKhoa sgk2 = new SachGiaoKhoa();
            sgk2.MaSach = "SGK002";
            sgk2.TenSach = "Văn 11";
            sgk2.TacGia = "Trần Thị B";
            sgk2.NamXuatBan = 2023;
            sgk2.GiaBan = 48000;
            sgk2.SoLuongTon = 40;
            sgk2.Lop = "11";
            sgk2.CoBanMau = true;
            _danhSachSach.Add(sgk2);

            SachThamKhao stk1 = new SachThamKhao();
            stk1.MaSach = "STK001";
            stk1.TenSach = "Lập trình C#";
            stk1.TacGia = "Nguyễn Văn C";
            stk1.NamXuatBan = 2024;
            stk1.GiaBan = 120000;
            stk1.SoLuongTon = 30;
            stk1.LinhVuc = "Công nghệ";
            _danhSachSach.Add(stk1);

            SachVanHoc svh1 = new SachVanHoc();
            svh1.MaSach = "VH001";
            svh1.TenSach = "Số đỏ";
            svh1.TacGia = "Vũ Trọng Phụng";
            svh1.NamXuatBan = 2018;
            svh1.GiaBan = 85000;
            svh1.SoLuongTon = 25;
            svh1.TheLoai = "Tiểu thuyết";
            _danhSachSach.Add(svh1);

            // Thêm khách hàng mẫu
            KhachHang kh1 = new KhachHang();
            kh1.MaKhach = "KH001";
            kh1.TenKhach = "Nguyễn Văn An";
            kh1.SoDienThoai = "0987654321";
            _danhSachKhach.Add(kh1);

            KhachHang kh2 = new KhachHang();
            kh2.MaKhach = "KH002";
            kh2.TenKhach = "Trần Thị Bình";
            kh2.SoDienThoai = "0978123456";
            _danhSachKhach.Add(kh2);
        }

        // Lấy danh sách sách
        public List<Sach> GetDanhSachSach()
        {
            return _danhSachSach;
        }

        // Thêm sách
        public void ThemSach(Sach sach)
        {
            _danhSachSach.Add(sach);
        }

        // Xóa sách
        public void XoaSach(string maSach)
        {
            Sach sach = null;
            foreach (var s in _danhSachSach)
            {
                if (s.MaSach == maSach)
                {
                    sach = s;
                    break;
                }
            }
            if (sach != null)
                _danhSachSach.Remove(sach);
        }

        // Tìm sách theo mã
        public Sach TimSachTheoMa(string maSach)
        {
            foreach (var s in _danhSachSach)
            {
                if (s.MaSach == maSach)
                    return s;
            }
            return null;
        }

        // Lấy danh sách khách hàng
        public List<KhachHang> GetDanhSachKhach()
        {
            return _danhSachKhach;
        }

        // Tạo hóa đơn
        public HoaDon TaoHoaDon(string maKhach, System.Collections.Generic.Dictionary<string, int> sachBan)
        {
            KhachHang khach = null;
            foreach (var k in _danhSachKhach)
            {
                if (k.MaKhach == maKhach)
                {
                    khach = k;
                    break;
                }
            }

            if (khach == null)
            {
                khach = new KhachHang();
                khach.MaKhach = "KH000";
                khach.TenKhach = "Khách lẻ";
            }

            HoaDon hoaDon = new HoaDon();
            hoaDon.KhachHang = khach;

            foreach (var item in sachBan)
            {
                Sach sach = TimSachTheoMa(item.Key);
                if (sach != null && sach.SoLuongTon >= item.Value)
                {
                    ChiTietHoaDon ct = new ChiTietHoaDon(sach, item.Value);
                    hoaDon.ChiTiet.Add(ct);
                    sach.SoLuongTon = sach.SoLuongTon - item.Value;
                }
            }

            _danhSachHoaDon.Add(hoaDon);
            return hoaDon;
        }

        // Lấy danh sách hóa đơn
        public List<HoaDon> GetDanhSachHoaDon()
        {
            return _danhSachHoaDon;
        }

        // Tổng số lượng sách trong kho
        public int GetTongSoLuongSach()
        {
            int tong = 0;
            foreach (var s in _danhSachSach)
            {
                tong += s.SoLuongTon;
            }
            return tong;
        }

        // Tổng doanh thu
        public double GetTongDoanhThu()
        {
            double tong = 0;
            foreach (var hd in _danhSachHoaDon)
            {
                tong += hd.TongTien;
            }
            return tong;
        }

        // Tổng số hóa đơn
        public int GetTongSoHoaDon()
        {
            return _danhSachHoaDon.Count;
        }
    }
}