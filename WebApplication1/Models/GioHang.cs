using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class GioHang
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid? UserId { get; set; }

        public User? User { get; set; }
        public List<ChiTietGioHang>? ChiTietGioHangs { get; set; }
    }
}
