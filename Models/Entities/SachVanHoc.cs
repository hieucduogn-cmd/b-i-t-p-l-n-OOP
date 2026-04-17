namespace baitaplon.Models.Entities
{
    public class SachVanHoc : Sach
    {
        public string TheLoai { get; set; }

        public override double TinhGiaSauChietKhau()
        {
            return GiaBan;
        }

        public override string GetLoaiSach()
        {
            return "Sách Văn Học";
        }
    }
}