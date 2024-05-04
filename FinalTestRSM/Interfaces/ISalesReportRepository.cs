using FinalTestRSM.Models;

namespace FinalTestRSM.Interfaces
{
    public interface ISalesReportRepository
    {
        /// <summary>
        /// Retrieves sales report data asynchronously based on specified parameters
        /// </summary>
        /// <param name="productCategory">The category of the product to filter the sales report data</param>
        /// <param name="startDate">The start date to filter the sales report data</param>
        /// <param name="endDate">The end date to filter the sales report data</param>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pageSize">The page size for pagination</param>
        /// <returns>A task representing the asynchronous operation that returns a list of sales report data</returns>
        Task<List<SalesReport>> GetSalesReportData(string productCategory, string startDate, string endDate, int pageNumber, int pageSize);
    }
}
