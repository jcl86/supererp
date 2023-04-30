using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperErp.Management.Domain;

namespace SuperErp.Data
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasMany(e => e.UserRoles)
                    .WithOne(x => x.Role)
                    .HasForeignKey(uc => uc.RoleId)
                    .IsRequired();
        }
    }
}
