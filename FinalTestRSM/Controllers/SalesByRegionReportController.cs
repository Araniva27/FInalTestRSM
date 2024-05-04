using FinalTestRSM.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalTestRSM.Controllers
{
    // Define the controller to handle HTTP requests related to sales by region reports
    [ApiController]
    [Route("api/[controller]")]
    public class SalesByRegionReportController: ControllerBase
    {
        private readonly ISalesByRegionReportService _service;

        // Constructor that injects the dependency ISalesByCustomerService 
        public SalesByRegionReportController(ISalesByRegionReportService service)
        {
            _service = service;
        }

        /// <summary>
        /// This endpoint retrieves sales data by region based on specified parameters
        /// </summary>
        /// <param name="productCategory">
        /// This parameter is used to filter the sales data by a specific product category.</param>
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
        /// The `GetSalesByRegionData` method returns an `IActionResult` which can be either an
        /// `OkObjectResult` with the sales data or a `StatusCodeResult` with a status code of 500 and
        /// an error message in case of an exception
        /// </returns>
        [HttpGet("SalesByRegionReport")]
        public async Task<IActionResult> GetSalesByRegionData([FromQuery] string? productCategory, [FromQuery] string? startDate, [FromQuery] string? endDate, [FromQuery] string? territory, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var salesByRegionData = await _service.GetSalesByRegionReportData(productCategory, startDate, endDate, territory, pageNumber, pageSize);
                return Ok(salesByRegionData);
            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
