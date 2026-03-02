using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchStoreApi.Models;

namespace WatchStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> Products = new List<Product>()
      {
            new Product { Id = 1, Name = "Watch A", Description = "Description for Watch A", Price = 199.99m },
            new Product { Id = 2, Name = "Watch B", Description = "Description for Watch B", Price = 299.99m },
            new Product { Id = 3, Name = "Watch C", Description = "Description for Watch C", Price = 399.99m }

      };
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return Products;
        }
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            Products.Add(product);
        }
        // api/products/id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if(existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
            }
        }

        // api/products/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                Products.Remove(existingProduct);
            }
        }
    }
}
