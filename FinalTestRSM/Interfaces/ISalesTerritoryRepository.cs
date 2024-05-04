using FinalTestRSM.Models;

namespace FinalTestRSM.Interfaces
{
    public interface ISalesTerritoryRepository
    {
        /// <summary>
        /// Retrieves sales territories asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of sales territories.</returns>
        Task<List<SalesTerritory>> GetSalesTerritories();
    }
}
