using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchStoreApi.Data;
using WatchStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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
        public async Task<IActionResult> Get()
        {
            var products = await dbContext.Products.ToListAsync();
            if(products == null || products.Count == 0)
            {
                return NotFound();
            }
            return  Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            if(product == null)
            {
                return BadRequest("Product is null");
            }
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // api/products/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                await dbContext.SaveChangesAsync();
                return Ok("Record updated");
            }
            return NotFound("Record not found");
        }

        // api/products/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                dbContext.Products.Remove(existingProduct);
                await dbContext.SaveChangesAsync();
                return Ok("Record has been deleted");
            }
            return NotFound("Record not found");
        }
    }
}
