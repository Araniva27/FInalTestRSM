using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FinalTestRSM.Infraestructure.Repositories
{
    public class SalesByCustomerRepository: ISalesByCustomerRepository
    {
        /// <summary>
        /// This class implements the ISalesByCustomerRepository interface and provides
        /// functionality for accessing sales data by customer.
        /// </summary>
        private readonly AdvWorksDbContext _context;
        /// <summary>
        /// Constructor for SalesByCustomerRepository class.
        /// </summary>
        /// <param name="context">An instance of AdvWorksDbContext for database access.</param>
        public SalesByCustomerRepository(AdvWorksDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieves sales data by customer asynchronously based on specified parameters
        /// </summary>
        /// <param name="customerName">The name of the customer to filter the sales data</param>
        /// <param name="productName">The name of the product to filter the sales data</param>
        /// <param name="startDate">The start date to filter the sales data</param>
        /// <param name="endDate">The end date to filter the sales data</param>
        /// <param name="pageNumber">The page number for pagination</param>
        /// <param name="pageSize">The page size for pagination</param>
        /// <returns>A task representing the asynchronous operation that returns a list of sales data by customer</returns>
        public async Task<List<SalesByCustomerReport>> GetSalesByCustomerData(string? customerName, string? productName, string? startDate, string? endDate, int pageNumber, int pageSize)
        {
            // Create SQL parameters to pass the values to the stored procedure
            var customerNameParam = new SqlParameter("@customer", customerName ?? (Object)DBNull.Value);
            var productNameParam = new SqlParameter("@product", productName ?? (Object)DBNull.Value);
            var startDateParam = new SqlParameter("@startDate", startDate ?? (Object)DBNull.Value);
            var endDateParam = new SqlParameter("@endDate", endDate ?? (Object)DBNull.Value);
            var pageNumberParam = new SqlParameter("@pageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@pageSize", pageSize);

            try
            {
                // Attempt to retrieve sales data by customer from the database
                var salesByCustomer = await _context.Set<SalesByCustomerReport>()
                                            .FromSqlRaw("EXEC SalesByCustomer @startDate, @endDate, @customer, @product, @pageNumber, @pageSize", startDateParam, endDateParam, customerNameParam, productNameParam, pageNumberParam, pageSizeParam)
                                             .ToListAsync();
                return salesByCustomer;
            }
            catch (Exception ex)
            {
                // If an exception occurs, wrap it in a new exception and rethrow
                throw new Exception(ex.ToString());
            }
        }
    }
}
