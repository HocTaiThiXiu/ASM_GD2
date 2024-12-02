using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class ChiTietHoaDonConfig : IEntityTypeConfiguration<ChiTietHoaDon>
    {
        public void Configure(EntityTypeBuilder<ChiTietHoaDon> builder)
        {
            builder.HasOne(c=>c.HoaDon).WithMany(c=>c.ChiTietHoaDons).HasForeignKey(c=>c.HoaDonId);
            builder.HasOne(c => c.SanPham).WithMany(c => c.ChiTietHoaDons).HasForeignKey(c => c.SanPhamId);
        }
    }
}
