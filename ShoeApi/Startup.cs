using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Middleware;
using Repository;
using Repository.EntityRepository;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoeApi", Version = "v1" });
            });
            services.AddDbContext<ShoeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SchoolContext"), b=>b.MigrationsAssembly("ShoeApi")));
            
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            });
            
            services.AddAutoMapper(typeof(UserMapper));
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IShoeRepository, ShoeRepository>();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IShoeService, ShoeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable CORS
            app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoeApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}