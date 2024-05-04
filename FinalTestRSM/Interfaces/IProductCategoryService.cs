using FinalTestRSM.Models;

namespace FinalTestRSM.Interfaces
{
    // This interface defines a service for retrieving product categories
    public interface IProductCategoryService
    {
        /// <summary>
        /// Retrieves all product categories asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of product categories</returns>
        Task<List<ProductCategory>> GetAllCategories();
    }
}
