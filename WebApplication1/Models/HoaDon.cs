using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class HoaDon
    {
        [Key]
        public Guid Id { get; set; }
        public decimal ThanhTien { get; set; }
        public DateTime? NgayTao { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public List<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}
