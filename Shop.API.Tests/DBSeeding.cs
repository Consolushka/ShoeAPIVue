
using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Data.Models;
using Shop.DataBase;
using Microsoft.EntityFrameworkCore;
using Type = Shop.Data.Models.Type;

namespace Shop.API.Tests
{
    public static class DBSeeding
    {
        private static DbContextOptions<ShopContext> _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;
        private static ShopContext _context;

        public static ShopContext CreateDb()
        {
            _context = new ShopContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            return _context;
        }
        
        private static void SeedDatabase()
        {
            SeedUsers();
            SeedTypes();
            SeedBrands();

            _context.SaveChanges();
            SeedBrandTypes();
            SeedGoods();
            SeedOrders();

            _context.SaveChanges();
        }
        
        private static void SeedUsers()
        {
            _context.Users.AddRange(new []{new User()
            {
                ConfirmString = new Guid(),
                Email = "consolushka@gmail.com",
                Address = "Listvenichnaya alleya 2A",
                Password = "CgwJ4C/o1BOl1hyEtdcTwg==",
                IsActive = true,
                UserName = "admin"
            },new User()
            {
                ConfirmString = new Guid(),
                Email = "newuser@gmail.com",
                UserName = "newUser",
                Address = "sdasd",
                Password = "wwetwwww"
            }});
        }
        
        private static void SeedTypes()
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
                }
            });
        }
        
        private static void SeedBrands()
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
        }

        private static void SeedBrandTypes()
        {
            _context.BrandTypes.AddRange(new List<BrandType>()
            {
                new BrandType()
                {
                    Brand = _context.Brands.FirstOrDefault(e=>e.Id==1),
                    Type = _context.Types.FirstOrDefault(e=>e.Id==1)
                },
                new BrandType()
                {
                    Brand = _context.Brands.FirstOrDefault(e=>e.Id==1),
                    Type = _context.Types.FirstOrDefault(e=>e.Id==2)
                },
                new BrandType()
                {
                    Brand = _context.Brands.FirstOrDefault(e=>e.Id==2),
                    Type = _context.Types.FirstOrDefault(e=>e.Id==1)
                }
            });
        }
        
        private static void SeedGoods()
        {
            _context.Goods.AddRange(new List<Good>()
            {
                new Good()
                {
                    Type = _context.Types.FirstOrDefault(e=>e.Id==1),
                    Name = "Nike v.1",
                    Brand = _context.Brands.FirstOrDefault(e=>e.Id==1),
                    PhotoFileName = "undefined.jpg"
                },
                new Good()
                {
                    Type = _context.Types.FirstOrDefault(e=>e.Id==2),
                    Name = "Nike t-shirt v.1",
                    Brand = _context.Brands.FirstOrDefault(e=>e.Id==1),
                    PhotoFileName = "undefined.jpg"
                },
                new Good()
                {
                    Type = _context.Types.FirstOrDefault(e=>e.Id==1),
                    Name = "Puma v.1",
                    Brand = _context.Brands.FirstOrDefault(e=>e.Id==2),
                    PhotoFileName = "undefined.jpg"
                },
            });
        }

        private static void SeedOrders()
        {
            _context.Orders.AddRange(new []
            {
                new Order()
                {
                    Status = 1,
                    UserId = 1,
                    IsPaid = false,
                    OrderTime = DateTime.Now.AddDays(-10)
                }
            });
        }
    }
}