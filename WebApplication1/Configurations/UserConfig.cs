using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;
namespace WebApplication1.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Tên").IsUnicode(true).IsFixedLength(true);
            builder.Property(x=>x.Password).HasColumnType("varchar 50").HasColumnName("Mật khẩu");
            builder.HasOne(c => c.GioHang).WithOne(c => c.User).HasForeignKey<GioHang>(c => c.UserId);
            builder.HasIndex(c=>c.Username).IsUnique();
        }
    }
}
