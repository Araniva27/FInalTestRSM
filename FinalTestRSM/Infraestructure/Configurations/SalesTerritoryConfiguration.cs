using FinalTestRSM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalTestRSM.Infraestructure.Configurations
{
    // Configuration class for the SalesTerritory entity
    public class SalesTerritoryConfiguration: IEntityTypeConfiguration<SalesTerritory>
    {
        // Configure method to define entity mappings
        public void Configure(EntityTypeBuilder<SalesTerritory> builder)
        {
            // Set the table name and schema
            builder.ToTable(nameof(SalesTerritory), "Sales");
            // Define the primary key
            builder.HasKey(e => e.TerritoryID);
            // Map the property to the database column and specify its name
            builder.Property(e => e.TerritoryID).HasColumnName("TerritoryID");
            // Make the Name property required
            builder.Property(e => e.Name).IsRequired();
        }
    }
}
