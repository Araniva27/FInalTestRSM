using FinalTestRSM.Infraestructure;
using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
namespace FinalTestRSM.Services
{
    /// <summary>
    /// This class implements the ISalesReportService interface and provides functionality
    /// for retrieving sales report data.
    /// </summary>
    public class SalesReportServices: ISalesReportService
    {
        private readonly ISalesReportRepository _repository;

        /// <summary>
        /// Constructor for SalesReportServices class.
        /// </summary>
        /// <param name="repository">An instance of ISalesReportRepository for data access.</param>
        public SalesReportServices(ISalesReportRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves sales report data asynchronously based on specified parameters
        /// </summary>
        /// <param name="productCategory">The category of the product to filter the sales data</param>
        /// <param name="startDate">The start date to filter the sales data</param>
        /// <param name="endDate">The end date to filter the sales data</param>
        /// <param name="pageNumber">The page number for pagination</param>
        /// <param name="pageSize">The page size for pagination</param>
        /// <returns>A task representing the asynchronous operation that returns a list of sales report data</returns>
        public async Task<List<SalesReport>> GetSalesReportData(string? productCategory, string? startDate, string? endDate, int pageNumber, int pageSize)
        {
            try
            {
                // Attempt to retrieve sales report data from the repository
                return await _repository.GetSalesReportData(productCategory, startDate, endDate, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                // If an exception occurs, wrap it in a new exception and rethrow
                throw new Exception(ex.ToString());
            }            
        }
    }
}
