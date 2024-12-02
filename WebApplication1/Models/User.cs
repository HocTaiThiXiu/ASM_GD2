using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Tên tối thiểu 10 ký tự")]
        [MaxLength(450, ErrorMessage = "Tên tối đa 450 ký tự")]
        public string? Name { get; set; }
        public DateTime NgaySinh { get; set; }
        [Required]
        [StringLength(50)]
        public string? Username { get; set; }
        [Required]
        [StringLength(50)]
        public string? Password { get; set; }
        [RegularExpression(@"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b$", ErrorMessage = "Không đúng định dạng số điện thoại")]
        public string? SDT { get; set; }
        public GioHang? GioHang { get; set; }
        public List<HoaDon>? HoaDons { get; set; }
    }
}
