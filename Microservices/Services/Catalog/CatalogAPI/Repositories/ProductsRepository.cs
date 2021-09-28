using System.Threading.Tasks;
using System.Collections.Generic;
using CatalogAPI.Entities;
using CatalogAPI.Data.Interfaces;
using CatalogAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace CatalogAPI.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ICatalogContext _context;

        public ProductsRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts() =>
            await _context.Products.Find(p => true).ToListAsync();

        public async Task<Product> GetProduct(string id) =>
            await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filterDefinision = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context.Products.Find(filterDefinision).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filterDefinision = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context.Products.Find(filterDefinision).ToListAsync();
        }

        public async Task<bool> TryCreateProduct(Product product)
        {
            try
            {
                await _context.Products.InsertOneAsync(product);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            ReplaceOneResult result = await _context.Products.ReplaceOneAsync(p=>p.Id == product.Id, product);
            bool isUpdated = result.IsAcknowledged && result.ModifiedCount > 0;
            return isUpdated;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            DeleteResult result = await _context.Products.DeleteOneAsync(p => p.Id == id);
            bool isDeleted = result.IsAcknowledged && result.DeletedCount > 0;
            return isDeleted;
        }
    }
}
