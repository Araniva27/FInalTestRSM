using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using FinalTestRSM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalTestRSM.Controllers
{
    // Define the controller to handle HTTP requests related to sales reports
    [ApiController]
    [Route("api/[controller]")]
    public class ReportSalesController:ControllerBase
    {
        private readonly ISalesReportService _service;

        // Constructor that injects the dependency ISalesReportService
        public ReportSalesController(ISalesReportService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get information about sales report    
        /// </summary>
        /// <param name="productCategory">
        /// This parameter is used to filter sales by product category
        /// </param>
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
        /// The method `GetSalesData` is returning an `IActionResult` which can be either an
        /// `OkObjectResult` with the sales data if the operation is successful, or a `StatusCodeResult`
        /// with status code 500 and an error message if an exception occurs during the operation.
        /// </returns>
        [HttpGet("SalesReport")]

        public async Task<IActionResult> GetSalesData([FromQuery] string? productCategory, [FromQuery] string? startDate, [FromQuery] string? endDate,[FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                // Call the service (SalesReportService) to get the report data
                var salesData = await _service.GetSalesReportData(productCategory, startDate, endDate, pageNumber, pageSize);
                // Return OK status along with the retrieved categories                
                return Ok(salesData);
            }catch(Exception ex) {
                // Return an internal server error (500) with the error message in case of exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }            
        }
    }
}
