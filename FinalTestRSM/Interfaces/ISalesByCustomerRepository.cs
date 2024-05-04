using FinalTestRSM.Models;

namespace FinalTestRSM.Interfaces
{
    public interface ISalesByCustomerRepository
    {
        /// <summary>
        /// Retrieves sales data by customer asynchronously based on specified parameters
        /// </summary>
        /// <param name="customerName">The name of the customer to filter the sales data</param>
        /// <param name="productName">The name of the product to filter the sales data</param>
        /// <param name="startDate">The start date to filter the sales data</param>
        /// <param name="endDate">The end date to filter the sales data</param>
        /// <param name="pageNumber">The page number for pagination</param>
        /// <param name="pageSize">The page size for pagination</param>
        /// <returns>A task representing the asynchronous operation that returns a list of sales by customer reports</returns>
        Task<List<SalesByCustomerReport>> GetSalesByCustomerData(string? customerName, string? productName, string? startDate, string? endDate, int pageNumber, int pageSize);
    }
}
