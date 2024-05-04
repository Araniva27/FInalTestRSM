using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;

namespace FinalTestRSM.Services
{
    /// <summary>
    /// This class implements the ISalesByRegionReportService interface and provides functionality
    /// for retrieving sales data by region report.
    /// </summary>
    public class SalesByRegionReportService: ISalesByRegionReportService
    {        
        private readonly ISalesByRegionReportRepository _repository;

        /// <summary>
        /// Constructor for SalesByRegionReportService class
        /// </summary>
        /// <param name="repository">An instance of ISalesByRegionReportRepository for data access</param>
        public SalesByRegionReportService(ISalesByRegionReportRepository repository)
        {
            _repository = repository;
        }

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
        public async Task<List<SalesByRegionReport>> GetSalesByRegionReportData(string? productCategory, string? startDate, string? endDate, string? regionName, int pageNumber, int pageSize)
        {
            try
            {
                // Attempt to retrieve sales data by region report from the repository
                return await _repository.GetSalesByRegionReportData(productCategory, startDate, endDate, regionName, pageNumber, pageSize);
            }catch (Exception ex)
            {
                // If an exception occurs, wrap it in a new exception and rethrow
                throw new Exception(ex.ToString());
            }
            
        }
    }
}
