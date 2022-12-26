using DataAccess;
using Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _dbContext;

        public ProductRepository(ICatalogContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task CreateProduct(Product product)
        {
            try
            {
                await _dbContext.Products.InsertOneAsync(product);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
                DeleteResult deleteResult = await _dbContext.Products.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProduct(string id)
        {
            try
            {
                var pro = await _dbContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (pro != null)
                {
                    return pro;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var pro = await _dbContext.Products.Find(x => true).ToListAsync();
                if (pro.Count > 0)
                {
                    return pro;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);
                var pros = await _dbContext.Products.Find(filter).ToListAsync();
                if(pros.Count > 0)
                {
                    return pros;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
                var pros = await _dbContext.Products.Find(filter).ToListAsync();
                if (pros.Count > 0)
                {
                    return pros;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var updateResult = await _dbContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
