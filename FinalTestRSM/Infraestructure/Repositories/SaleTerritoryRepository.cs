using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalTestRSM.Infraestructure.Repositories
{
    /// <summary>
    /// This class implements the ISalesTerritoryRepository interface and provides
    /// functionality for accessing sales territories.
    /// </summary>
    public class SaleTerritoryRepository: ISalesTerritoryRepository
    {
        private readonly AdvWorksDbContext _context;
        /// <summary>
        /// Constructor for SaleTerritoryRepository class.
        /// </summary>
        /// <param name="context">An instance of AdvWorksDbContext for database access.</param>
        public SaleTerritoryRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalesTerritory>> GetSalesTerritories()
        {
            try
            {
                // Retrieve sales territories from the database without tracking changes
                var salesTerritories = await _context.Set<SalesTerritory>()
                                                .AsNoTracking()
                                                .ToListAsync();
                return salesTerritories;
            }catch(Exception ex) {
                // If an exception occurs, wrap it in a new exception and rethrow
                throw new Exception(ex.ToString());
            }
        }
    }
}
