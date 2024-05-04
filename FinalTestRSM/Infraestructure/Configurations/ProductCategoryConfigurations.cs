using FinalTestRSM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalTestRSM.Infraestructure.Configurations
{
    // Configuration class for the ProductCategory entity
    public class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
    {
        // Configure method to define entity mappings
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            // Set the table name and schema
            builder.ToTable(nameof(ProductCategory), "Production");
            // Define the primary key
            builder.HasKey(e => e.productCategoryID);
            // Map the property to the database column and specify its name
            builder.Property(e => e.productCategoryID).HasColumnName("ProductCategoryID");
            // Make the Name property required
            builder.Property(e => e.Name).IsRequired();
        }
    }
}
