using System;
using System.Collections.Generic;
using System.Linq;

namespace baitaplon.Models.Entities
{
    public class HoaDon
    {
        public string MaHoaDon { get; set; }
        public KhachHang KhachHang { get; set; }
        public DateTime NgayLap { get; set; }
        public List<ChiTietHoaDon> ChiTiet { get; set; }

        public double TongTien
        {
            get
            {
                double tong = 0;
                foreach (var ct in ChiTiet)
                    tong += ct.ThanhTien;
                return tong;
            }
        }

        public HoaDon()
        {
            MaHoaDon = "HD" + DateTime.Now.Ticks.ToString();
            NgayLap = DateTime.Now;
            ChiTiet = new List<ChiTietHoaDon>();
        }
    }
}
