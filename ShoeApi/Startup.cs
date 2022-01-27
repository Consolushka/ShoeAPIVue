using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Middleware;
using WebApplication.Repository;
using WebApplication.Repository.Contracts;
using WebApplication.Repository.EntityRepository;
using WebApplication.Services.Contracts;
using WebApplication.Services.ModelServices;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });
            
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            
            services.AddRouting(options => options.LowercaseUrls = true);
            
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            
            services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SchoolContext"), b=>b.MigrationsAssembly("ShoeApi")));
            
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            });
            
            services.AddAutoMapper(typeof(UserMapper));
            services.AddAutoMapper(typeof(BrandMapper));
            services.AddAutoMapper(typeof(GoodMapper));
            
            services.AddScoped<BaseRepository<User>, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<BaseRepository<Brand>,BrandRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            
            services.AddScoped<BaseRepository<Good>,GoodsRepository>();
            services.AddScoped<IGoodsRepository, GoodsRepository>();
            
            services.AddScoped<BaseRepository<Log>,LogRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IGoodsService, GoodsService>();
            services.AddScoped<ILogService, LogService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory factory)
        {
            //Enable CORS
            app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json", 
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();
            app.ConfigurationBuildInException(factory);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"Photos")),
                RequestPath = "/Photos"
            });
            
            DataInitializer.SeedData(app,Configuration);
        }
    }
}