using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? SanPhamId { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public Guid? HoaDonId { get; set; }
        public SanPham? SanPham { get; set; }
        public HoaDon? HoaDon { get; set; }
    }
}
