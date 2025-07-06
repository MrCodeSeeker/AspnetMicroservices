using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    
    public class ProductRepositories : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepositories(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }
       

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id==id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await _context.Products.Find(p => p.Category == categoryName).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Products.Find(p => p.Name == name).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var result= await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> DeleteProduct(string id)
        {
            var result = await _context.Products.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
