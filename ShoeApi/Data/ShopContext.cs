using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Models;

namespace WebApplication.Data
{
    public class ShopContext: DbContext
    {
        
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<BrandType> BrandTypes { get; set; }
    }
}