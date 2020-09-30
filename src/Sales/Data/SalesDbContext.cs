using Microsoft.EntityFrameworkCore;

namespace Sales.Data
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options)
        {
        }
    }
}
