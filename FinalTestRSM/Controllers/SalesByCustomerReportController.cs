using FinalTestRSM.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalTestRSM.Controllers
{
    // Define the controller to handle HTTP requests related to sales by customers reports
    [ApiController]
    [Route("api/[controller]")]
    public class SalesByCustomerReportController: ControllerBase
    {
        private readonly ISalesByCustomerService _service;

        // Constructor that injects the dependency ISalesByCustomerService 
        public SalesByCustomerReportController(ISalesByCustomerService service)
        {
            _service = service;
        }

        /// <summary>
        /// This endpoint retrieves sales data by customer based on specified parameters       
        /// </summary>
        /// <param name="customerName">
        /// Parameter used to filter sales data by a specific customer's name. If provided, the API will return sales data related to the
        /// specified customer only.</param>
        /// <param name="productName">
        /// This parameter is used to filter the sales data based on a specific product name.
        /// data related to that specific product.</param>
        /// <param name="startDate">
        /// This parameter is used to specify the start date for filtering sales data
        /// </param>
        /// <param name="endDate">
        /// This parameter is used to specify the end date for filtering sales data    
        /// </param>
        /// <param name="pageNumber">T
        /// This parameter is used to specify the page number of the sales data to be retrieved. 
        /// </param>
        /// <param name="pageSize">
        /// This parameter specifies the number of items to be included in each page of the sales report data. 
        /// </param>
        /// <returns>
        /// The `GetSalesByCustomer` method returns an `IActionResult`. If the method executes
        /// successfully, it returns an `OkObjectResult` with the sales data for the specified customer.
        /// If an exception occurs during the execution, it returns a `StatusCodeResult` with status
        /// code 500 and an error message indicating an internal server error.
        /// </returns>
        [HttpGet("SalesByCustomer")]

        public async Task<IActionResult> GetSalesByCustomer([FromQuery] string? customerName, [FromQuery] string? productName, [FromQuery] string? startDate, [FromQuery] string? endDate, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                // Call the service (SalesByCustomer) to get the report data
                var salesByCustomer = await _service.GetSalesByCustomerData(customerName, productName, startDate, endDate, pageNumber, pageSize);
                // Return OK status along with the retrieved categories
                return Ok(salesByCustomer);
            }
            catch (Exception ex)
            {
                // Return an internal server error (500) with the error message in case of exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
