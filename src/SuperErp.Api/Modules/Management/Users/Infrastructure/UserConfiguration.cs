using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperErp.Management.Domain;

namespace SuperErp.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

            builder.HasMany(e => e.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
