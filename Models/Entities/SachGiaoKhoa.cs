namespace baitaplon.Models.Entities
{
    public class SachGiaoKhoa : Sach
    {
        public string Lop { get; set; }
        public bool CoBanMau { get; set; }

        public override double TinhGiaSauChietKhau()
        {
            if (CoBanMau)
                return GiaBan * 0.9;
            return GiaBan;
        }

        public override string GetLoaiSach()
        {
            return "Sách Giáo Khoa";
        }
    }
}