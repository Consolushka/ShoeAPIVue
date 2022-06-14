using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Type = Shop.Data.Models.Type;

namespace Shop.DataBase
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
                
                SeedStoresIfNone();
                _context.SaveChanges();
                
                SeedStockItemsIfNone();
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
                    Password = "CgwJ4C/o1BOl1hyEtdcTwg==",
                    IsActive = true,
                    IsAdmin = true,
                    UserName = "admin"
                });
                _context.SaveChanges();
            }

            if (!_context.Baskets.Any())
            {
                _context.Baskets.Add(new Basket()
                {
                    User = _context.Users.Find((long)1)
                });
                _context.SaveChanges();
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

        private static void SeedStoresIfNone()
        {
            if (!_context.Stores.Any())
            {
                _context.Stores.Add(new Store()
                {
                    Address = "Internacionlnaya 54",
                    Name = "Nearest"
                });
            }
        }

        private static void SeedStockItemsIfNone()
        {
            if (!_context.StockItems.Any())
            {
                _context.StockItems.AddRange(new List<StockItem>()
                {
                    new StockItem()
                    {
                        Good = _context.Goods.Find((long)1),
                        Store = _context.Stores.Find((long)1),
                        Count = 100,
                        Price = 220.31
                    },
                    new StockItem()
                    {
                        Good = _context.Goods.Find((long)2),
                        Store = _context.Stores.Find((long)1),
                        Count = 250,
                        Price = 115
                    },
                    new StockItem()
                    {
                        Good = _context.Goods.Find((long)3),
                        Store = _context.Stores.Find((long)1),
                        Count = 55,
                        Price = 1000
                    }
                });
            }
        }
    }   
}