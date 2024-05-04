using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FinalTestRSM.Infraestructure.Repositories
{
    /// <summary>
    /// This class implements the ISalesByRegionReportRepository interface and provides
    /// functionality for accessing sales data by region report
    /// </summary>
    public class SaleByRegionReportRepository: ISalesByRegionReportRepository
    {
        private readonly AdvWorksDbContext _context;

        /// <summary>
        /// Constructor for SaleByRegionReportRepository class
        /// </summary>
        /// <param name="context">An instance of AdvWorksDbContext for database access</param>
        public SaleByRegionReportRepository(AdvWorksDbContext context)
        {
            _context = context;
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
        /// <returns>A task representing the asynchronous operation that returns a list of sales data by region report</returns>
        public async Task<List<SalesByRegionReport>> GetSalesByRegionReportData(string productCategory, string startDate, string endDate, string regionName, int pageNumber, int pageSize)
        {
            // Create SQL parameters to pass the values to the stored procedure
            var productCategoryParam = new SqlParameter("@productCategory", productCategory ?? (Object)DBNull.Value);
            var startDateParam = new SqlParameter("@startDate", startDate ?? (Object)DBNull.Value);
            var endDateParam = new SqlParameter("@endDate", endDate ?? (Object)DBNull.Value);
            var regionNameParam = new SqlParameter("@territory", regionName ?? (Object)DBNull.Value);
            var pageNumberParam = new SqlParameter("@pageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@pageSize", pageSize);

            try
            {
                // Attempt to retrieve sales data by region report from the database
                var salesByRegionData = await _context.Set<SalesByRegionReport>()
                                              .FromSqlRaw("EXEC Sales_CTE @productCategory, @startDate, @endDate, @territory, @pageNumber, @pageSize", productCategoryParam, startDateParam, endDateParam, regionNameParam, pageNumberParam, pageSizeParam)
                                              .ToListAsync();
                return salesByRegionData;
            }
            catch (Exception ex)
            {
                // If an exception occurs, wrap it in a new exception and rethrow
                throw new Exception(ex.ToString());
            }
        }
    }
}
