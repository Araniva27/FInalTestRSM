using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using Microsoft.Identity.Client;

namespace FinalTestRSM.Services
{
    /// <summary>
    /// This C# class implements the ISalesByCustomerService interface and provides functionality
    /// for retrieving sales data by customer
    /// </summary>
    public class SalesByCustomerService: ISalesByCustomerService
    {
        private readonly ISalesByCustomerRepository _repository;

        /// <summary>
        /// Constructor for SalesByCustomerService class
        /// </summary>
        /// <param name="repository">An instance of ISalesByCustomerRepository for data access</param>
        public SalesByCustomerService(ISalesByCustomerRepository repository)
        {
            _repository = repository;
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
        /// <returns>A task representing the asynchronous operation that returns a list of sales by customer reports</returns>
        public async Task<List<SalesByCustomerReport>> GetSalesByCustomerData(string? customerName, string? productName, string? startDate, string? endDate, int pageNumber, int pageSize)
        {
            try
            {
                // Call repository (SalesByCustomerRepository) to retrieve information
                return await _repository.GetSalesByCustomerData(customerName, productName, startDate, endDate, pageNumber, pageSize);
            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
