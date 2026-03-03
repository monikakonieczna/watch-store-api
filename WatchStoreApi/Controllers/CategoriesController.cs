using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchStoreApi.Data;
using WatchStoreApi.Models;

namespace WatchStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private ApiDbContext dbContext;
        public CategoriesController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var categories = await dbContext.Categories.ToListAsync();
           return Ok(categories);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category is null");
            }
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }
            existingCategory.Name = category.Name;
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "Record updated");
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }
            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return Ok("Record has been deleted");
        }
    }
}
