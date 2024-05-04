using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FinalTestRSM.Infraestructure.Repositories
{
    /// <summary>
    /// This class implements the ISalesReportRepository interface and provides
    /// functionality for accessing sales report data.
    /// </summary>
    public class SaleReportRepository: ISalesReportRepository
    {

        private readonly AdvWorksDbContext _context;

        /// <summary>
        /// Constructor for SaleReportRepository class.
        /// </summary>
        /// <param name="context">An instance of AdvWorksDbContext for database access.</param>
        public SaleReportRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves sales report data asynchronously based on specified parameters
        /// </summary>
        /// <param name="productCategory">The category of the product to filter the sales report data</param>
        /// <param name="startDate">The start date to filter the sales report data</param>
        /// <param name="endDate">The end date to filter the sales report data</param>
        /// <param name="pageNumber">The page number for pagination</param>
        /// <param name="pageSize">The page size for pagination</param>
        /// <returns>A task representing the asynchronous operation that returns a list of sales report data</returns>
        public async Task<List<SalesReport>> GetSalesReportData(string productCategory, string startDate, string endDate,int pageNumber, int pageSize)
        {
            // Create SQL parameters to pass the values to the stored procedure
            var productCategoryParam = new SqlParameter("@productCategory", productCategory ?? (object)DBNull.Value);
            var startDateParam = new SqlParameter("@startDate", startDate ?? (object)DBNull.Value);
            var endDateParam = new SqlParameter("@endDate", endDate ?? (object)DBNull.Value);
            var pageNumberParam = new SqlParameter("@pageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@pageSize", pageSize);

            try
            {
                // Attempt to retrieve sales report data from the database
                var saleReportData = await _context.Set<SalesReport>()
                                            .FromSqlRaw("EXEC SalesReport @productCategory, @startDate, @endDate, @pageNumber, @pageSize", productCategoryParam, startDateParam, endDateParam, pageNumberParam, pageSizeParam)                                                                                        
                                            .ToListAsync();

                return saleReportData;
            }
            catch (Exception ex)
            {
                // If an exception occurs, wrap it in a new exception and rethrow
                throw new Exception(ex.ToString());
            }
        }
    }
}
