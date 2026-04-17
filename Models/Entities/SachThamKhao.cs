namespace baitaplon.Models.Entities
{
    public class SachThamKhao : Sach
    {
        public string LinhVuc { get; set; }

        public override double TinhGiaSauChietKhau()
        {
            return GiaBan * 0.95;
        }

        public override string GetLoaiSach()
        {
            return "Sách Tham Khảo";
        }
    }
}