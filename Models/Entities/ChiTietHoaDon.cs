namespace baitaplon.Models.Entities
{
    public class ChiTietHoaDon
    {
        public Sach Sach { get; set; }
        public int SoLuong { get; set; }

        public double ThanhTien
        {
            get { return Sach.TinhGiaSauChietKhau() * SoLuong; }
        }

        public ChiTietHoaDon(Sach sach, int soLuong)
        {
            Sach = sach;
            SoLuong = soLuong;
        }
    }
}