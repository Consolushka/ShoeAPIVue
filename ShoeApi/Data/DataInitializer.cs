using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Middleware;
using Type = WebApplication.Data.Models.Type;

namespace WebApplication.Data
{
    public static class DataInitializer
    {

        private static IApplicationBuilder _builder;
        private static IConfiguration _configuration;
        private static ShopContext _context;

        public static void SeedData(IApplicationBuilder builder, IConfiguration configuration)
        {
            _builder = builder;
            _configuration = configuration;
            using (var serviceScope = _builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ShopContext>();
                _context = context;
                SeedGoodsTypes();
                SeedUsersIfNone();
                SeedBrandsIfNone();
                
                _context.SaveChanges();
                
                SeedGoodsIfNone();

                _context.SaveChanges();

                SeedBrandTypes();

                _context.SaveChanges();
            }
        }

        private static void SeedGoodsTypes()
        {
            if (!_context.Types.Any())
            {
                _context.Types.AddRange(new List<Type>()
                {
                    new Type()
                    {
                        Name = "Shoe"
                    },
                    new Type()
                    {
                        Name = "T-Shirt"
                    },
                });
            }
        }

        private static void SeedUsersIfNone()
        {
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User()
                {
                    ConfirmString = new Guid(),
                    Email = "consolushka@gmail.com",
                    Address = "Listvenichnaya alleya 2A",
                    Password = _configuration.Encode("admin"),
                    IsActive = true,
                    UserName = "admin"
                });
            }
        }
        
        private static void SeedBrandsIfNone()
        {
            if (!_context.Brands.Any())
            {
                _context.Brands.AddRange(new List<Brand>()
                {
                    new Brand()
                    {
                        Name = "Nike"
                    },
                    new Brand()
                    {
                        Name = "Puma"
                    },
                });
                _context.SaveChanges();
            }
        }

        private static void SeedBrandTypes()
        {
            if (!_context.BrandTypes.Any())
            {
                _context.BrandTypes.AddRange(new List<BrandType>()
                {
                    new BrandType()
                    {
                        Brand = _context.Brands.Find((long)1),
                        Type = _context.Types.Find((long)1),
                    },
                    new BrandType()
                    {
                        Brand = _context.Brands.Find((long)1),
                        Type = _context.Types.Find((long)2),
                    },
                    new BrandType()
                    {
                        Brand = _context.Brands.Find((long)2),
                        Type = _context.Types.Find((long)1),
                    },
                });
            }
        }
        
        private static void SeedGoodsIfNone()
        {
            if (!_context.Goods.Any())
            {
                _context.Goods.AddRange(new List<Good>()
                {
                    new Good()
                    {
                        Name = "Nike v.1",
                        BrandId = 1,
                        TypeId = 1,
                        PhotoFileName = "undefined.jpg"
                    },
                    new Good()
                    {
                        Name = "Nike T-Shirt",
                        BrandId = 1,
                        TypeId = 2,
                        PhotoFileName = "undefined.jpg"
                    },
                    new Good()
                    {
                        Name = "Puma v.1",
                        BrandId = 2,
                        TypeId = 1,
                        PhotoFileName = "undefined.jpg"
                    },
                });
            }
        }
    }
}