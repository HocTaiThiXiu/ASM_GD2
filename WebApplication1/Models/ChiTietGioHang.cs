using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ChiTietGioHang
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? GioHangId { get; set; }
        public Guid? SanPhamId {  get; set; }
        public int SoLuong { get; set; }
        public GioHang? GioHang { get; set; }
        public SanPham? SanPham { get; set; }

    }
}
