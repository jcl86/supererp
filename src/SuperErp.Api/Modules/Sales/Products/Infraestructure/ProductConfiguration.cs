using SuperErp.Sales.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace SuperErp.Data
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(Sales.Model.Restrictions.Products.NameMaxLength);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(Sales.Model.Restrictions.Products.DescriptionMaxLength);
            builder.Property(x => x.Price).HasPrecision(12, 2).HasDefaultValue(0);

            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
