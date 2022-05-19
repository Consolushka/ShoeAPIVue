using Shop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Type = Shop.Data.Models.Type;

namespace Shop.DataBase
{
    public class ShopContext: DbContext
    {
        //dotnet ef --startup-project Shop.Api/Shop.Api.csproj migrations add AddedOrders -p Shop.DataBase/Shop.DataBase.csproj
        //dotnet ef --startup-project Shop.Api/Shop.Api.csproj database update
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
        public DbSet<Order> Orders { get; set; }
    }
}