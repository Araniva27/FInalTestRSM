using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using FinalTestRSM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FinalTestRSM.Controllers
{
    // ApiController attribute denotes this class as a controller for handling HTTP API requests
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController:ControllerBase
    {
        private readonly IProductCategoryService _service;

        // Constructor injection to provide an instance of IProductCategoryService
        public ProductCategoryController(IProductCategoryService service)
        {
            _service = service;
        }

        // Endpoint for getting all product categories
        /// <summary>
        /// Get all products categories        
        /// </summary>
        /// <returns>
        /// The GetSalesData method is returning a list of all product categories retrieved from the
        /// ProductCategoryService. If the retrieval is successful, it returns an OK status along with
        /// the categories. If an exception occurs during the process, it returns a 500 Internal Server
        /// Error status along with an error message indicating the internal server error.
        /// </returns>
        [HttpGet("AllCategories")]
        public async Task<IActionResult> GetSalesData()
        {
            try
            {
                // Call the service (ProductCategoryService) to get all product categories
                var categories = await _service.GetAllCategories();
                // Return OK status along with the retrieved categories
                return Ok(categories);
            }catch(Exception ex) {
                // Return 500 Internal Server Error status if an exception occurs
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }                        
        }
    }
}
