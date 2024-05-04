using FinalTestRSM.Interfaces;
using FinalTestRSM.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FinalTestRSM.Infraestructure.Repositories
{
    /// <summary>
    /// This class implements the IProductCategoryRepository interface and provides
    /// functionality for accessing product categories.
    /// </summary>
    public class ProductCategoryRepository: IProductCategoryRepository
    {

        private readonly AdvWorksDbContext _context;

        /// <summary>
        /// Constructor for ProductCategoryRepository class
        /// </summary>
        /// <param name="context">An instance of AdvWorksDbContext for database access</param>
        public ProductCategoryRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all product categories asynchronously
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of product categories</returns>
        public async Task<List<ProductCategory>> GetAllCategories()
        {
            try{
                // Attempt to retrieve all product categories from the database
                var categories = await  _context.Set<ProductCategory>()
                                                .AsNoTracking()
                                                .ToListAsync();
                return categories;
            }catch(Exception ex){
                // If an exception occurs, wrap it in a new exception and rethrow
                throw new Exception(ex.ToString());
            }
        }
    }
}
