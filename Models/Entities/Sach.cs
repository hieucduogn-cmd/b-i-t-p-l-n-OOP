using System;

namespace baitaplon.Models.Entities
{
    public abstract class Sach
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public int NamXuatBan { get; set; }
        public double GiaBan { get; set; }
        public int SoLuongTon { get; set; }

        public abstract double TinhGiaSauChietKhau();
        public abstract string GetLoaiSach();
    }
}