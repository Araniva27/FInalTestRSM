using FinalTestRSM.Models;

namespace FinalTestRSM.Interfaces
{
    public interface ISalesByRegionReportRepository
    {
        /// <summary>
        /// Retrieves sales data by region report asynchronously based on specified parameters
        /// </summary>
        /// <param name="productCategory">The category of the product to filter the sales data</param>
        /// <param name="startDate">The start date to filter the sales data</param>
        /// <param name="endDate">The end date to filter the sales data</param>
        /// <param name="regionName">The name of the region to filter the sales data</param>
        /// <param name="pageNumber">The page number for pagination</param>
        /// <param name="pageSize">The page size for pagination</param>
        /// <returns>A task representing the asynchronous operation that returns a list of sales by region reports</returns>
        Task<List<SalesByRegionReport>> GetSalesByRegionReportData(string productCategory, string startDate, string endDate, string regionName, int pageNumber, int pageSize);
    }
}
