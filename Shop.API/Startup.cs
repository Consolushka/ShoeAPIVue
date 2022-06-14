using System.IO;
using System.Linq;
using System.Web.Http;
using Shop.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shop.API.Core;
using Shop.MiddleWare;
using Shop.Repositories;
using Shop.Services;

namespace Shop.API
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
                options.UseSqlServer(Configuration.GetConnectionString("SchoolContext"), b=>b.MigrationsAssembly("Shop.DataBase")));
            
            
            //Enable CORS
            var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy  =>
                    {
                        policy.WithOrigins("http://localhost:8080")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                            
                    });
            });
            
            services.AddAutoMapper(typeof(Mapper));

            ReposDependency.CreateDependency(services);
            
            ServicesDependency.CreateDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory factory)
        {
            app.Use((context, next) =>
            {
                if (context.Request.Headers.Any(k => k.Key.Contains("Origin")) && context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                    return context.Response.WriteAsync("handled");
                }

                return next.Invoke();
            });
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
//keep the middleware order.
            
            app.UseCors("_myAllowSpecificOrigins");
            app.UseCors(c => c.AllowAnyHeader());

            app.UseAuthentication();
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
            
            DataInitializer.SeedData(app, Configuration);
        }
    }
}