using Microsoft.Extensions.DependencyInjection;
using Shop.Services.Contracts;
using Shop.Services.ModelServices;

namespace Shop.Services
{
    public static class ServicesDependency
    {
        public static void CreateDependencies(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IBrandTypeService, BrandTypeService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IGoodsService, GoodsService>();
            services.AddScoped<ILogService, LogService>();
        }
    }
}