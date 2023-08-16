using Microsoft.EntityFrameworkCore;
using ReactQuery_Server.Data.Models;

namespace ReactQuery_Server.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
