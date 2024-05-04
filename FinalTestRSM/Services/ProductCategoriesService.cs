using FinalTestRSM.Infraestructure;
using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
namespace FinalTestRSM.Services
{
    
    public class ProductCategoryService: IProductCategoryService
    {        
        private readonly IProductCategoryRepository _repository;

        /// <summary>
        /// Constructor for ProductCategoryService class
        /// </summary>
        /// <param name="repository">An instance of IProductCategoryRepository for data access</param>
        public ProductCategoryService(IProductCategoryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all product categories asynchronously
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of product categories</returns>
        public async Task<List<ProductCategory>> GetAllCategories()
        {
            try
            {
                return await _repository.GetAllCategories();
            }catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
        }
        
    }
}
