using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class GioHangChiTietConfig : IEntityTypeConfiguration<ChiTietGioHang>
    {
        public void Configure(EntityTypeBuilder<ChiTietGioHang> builder)
        {
            builder.HasOne(c => c.GioHang).WithMany(c => c.ChiTietGioHangs).HasForeignKey(c => c.GioHangId);
            builder.HasOne(c => c.SanPham).WithMany(c => c.chiTietGioHangs).HasForeignKey(c => c.SanPhamId);
        }
    }
}
