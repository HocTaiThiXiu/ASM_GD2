using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SanPham
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? ImgUrl { get; set; }
        public int SoLuong {  get; set; }
        public ICollection<ChiTietGioHang>? chiTietGioHangs{ get; set; }
        public ICollection<ChiTietHoaDon>? ChiTietHoaDons{ get; set; }
    }
}
