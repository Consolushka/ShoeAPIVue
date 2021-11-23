using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Models;

namespace WebApplication.Data
{
    public class ShoeContext: DbContext
    {
        
        public ShoeContext(DbContextOptions<ShoeContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}