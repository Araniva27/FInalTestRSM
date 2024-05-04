namespace FinalTestRSM.Infraestructure
{
    using System.Reflection;
    using FinalTestRSM.Models;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    // Define the database context
    public class AdvWorksDbContext: DbContext
    {
        public AdvWorksDbContext()
        {
        }
        // Constructor taking DbContextOptions to configure the database context
        public AdvWorksDbContext(DbContextOptions<AdvWorksDbContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for model entities
        // Virtual table for the SalesReport entity
        public virtual DbSet<SalesReport> SalesReport { get; set; }
        // Virtual table for the ProductCategory entity
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        // Virtual table for the SalesByRegionReport entity
        public virtual DbSet<SalesByRegionReport> SalesByRegions { get; set; }
        // Virtual table for the SalesTerritory entity
        public virtual DbSet<SalesTerritory> SalesTerrritories { get; set; }
        // Virtual table for the SalesByCustomerReport entity
        public virtual DbSet<SalesByCustomerReport> SalesByCustomer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Configure entities with no primary key (NoKey)
            // The SalesReport entity has no defined primary key
            modelBuilder.Entity<SalesReport>().HasNoKey();
            // The SalesByRegionReport entity has no defined primary key
            modelBuilder.Entity<SalesByRegionReport>().HasNoKey();
            // The SalesByCustomerReport entity has no defined primary key
            modelBuilder.Entity<SalesByCustomerReport>().HasNoKey();
        }
    }
}
