using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;

namespace FinalTestRSM.Services
{
    /// <summary>
    /// This class implements the ISalesTerritoryService interface and provides functionality
    /// for retrieving sales territories.
    /// </summary>
    public class SaleTerritoryService: ISalesTerritoryService
    {
        private readonly ISalesTerritoryRepository _repository;

        /// <summary>
        /// Constructor for SaleTerritoryService class
        /// </summary>
        /// <param name="salesTerritoryRepository">An instance of ISalesTerritoryRepository for data access</param>
        public SaleTerritoryService(ISalesTerritoryRepository salesTerritoryRepository)
        {
            _repository = salesTerritoryRepository;
        }

        /// <summary>
        /// Retrieves sales territories asynchronously
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of sales territories</returns>
        public async Task<List<SalesTerritory>> GetSalesTerritories()
        {
            try
            {
                // Attempt to retrieve sales territories from the repository
                return await _repository.GetSalesTerritories();
            }catch (Exception ex)
            {
                // If an exception occurs, wrap it in a new exception and rethrow
                throw new Exception(ex.ToString());
            }
            
        }
    }
}
