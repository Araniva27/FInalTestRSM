using FinalTestRSM.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalTestRSM.Controllers
{
     // Define the controller to handle HTTP requests related to sales by region report
    [ApiController]
    [Route("api/[controller]")]
    public class SalesTerritoriesController: ControllerBase
    {
        private readonly ISalesTerritoryService _service;

        // Constructor that injects the dependency ISalesTerritoryService
        public SalesTerritoriesController(ISalesTerritoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// This endpoint retrieves data about the territories
        /// </summary>
        /// <returns>
        /// The GetSalesTerritories method is returning a list of sales territories. If the method
        /// executes successfully, it will return an Ok response with the list of territories. If an
        /// exception occurs during the execution, it will return a 500 Internal Server Error response
        /// with the error message.
        /// </returns>
        [HttpGet("AllTerritories")]
        public async Task<IActionResult> GetSalesTerritories()
        {
            try
            {
                var territories = await _service.GetSalesTerritories();
                return Ok(territories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
