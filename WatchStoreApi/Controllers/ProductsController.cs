using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchStoreApi.Data;
using WatchStoreApi.Models;

namespace WatchStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ApiDbContext dbContext;
        public ProductsController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return dbContext.Products;
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return dbContext.Products.FirstOrDefault(p=>p.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        // api/products/id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            var existingProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if(existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                dbContext.SaveChanges();
            }
        }

        // api/products/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existingProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                dbContext.Products.Remove(existingProduct);
                dbContext.SaveChanges();
            }
        }
    }
}
