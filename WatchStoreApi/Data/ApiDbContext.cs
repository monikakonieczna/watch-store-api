using Microsoft.EntityFrameworkCore;
using WatchStoreApi.Models;

namespace WatchStoreApi.Data
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
