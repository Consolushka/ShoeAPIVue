using Shop.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Shop.Repositories.Basis;
using Shop.Repositories.Contracts;
using Shop.Repositories.ModelRepositories;
using Type = Shop.Data.Models.Type;

namespace Shop.Repositories
{
    public static class ReposDependency
    {
        public static void CreateDependency(IServiceCollection services)
        {
            services.AddScoped<BaseRepository<User>, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<BaseRepository<Brand>,BrandRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            
            services.AddScoped<BaseRepository<BrandType>,BrandTypeRepository>();
            services.AddScoped<IBrandTypeRepository, BrandTypeRepository>();
            
            services.AddScoped<BaseRepository<Good>,GoodsRepository>();
            services.AddScoped<IGoodsRepository, GoodsRepository>();
            
            services.AddScoped<BaseRepository<Log>,LogRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            
            services.AddScoped<BaseRepository<Type>,TypeRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}