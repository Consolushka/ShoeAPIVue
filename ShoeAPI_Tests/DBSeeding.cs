using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Models;
using Type = WebApplication.Data.Models.Type;

namespace ShoeAPI_Tests
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

            _context.SaveChanges();
        }
        
        private static void SeedUsers()
        {
            _context.Users.Add(new User()
            {
                ConfirmString = new Guid(),
                Email = "consolushka@gmail.com",
                Address = "Listvenichnaya alleya 2A",
                Password = "CgwJ4C/o1BOl1hyEtdcTwg==",
                IsActive = true,
                UserName = "admin"
            });
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
                    BrandId = 1,
                    PhotoFileName = "undefined.jpg"
                },
                new Good()
                {
                    Type = _context.Types.FirstOrDefault(e=>e.Id==2),
                    Name = "Nike t-shirt v.1",
                    BrandId = 1,
                    PhotoFileName = "undefined.jpg"
                },
                new Good()
                {
                    Type = _context.Types.FirstOrDefault(e=>e.Id==1),
                    Name = "Puma v.1",
                    BrandId = 2,
                    PhotoFileName = "undefined.jpg"
                },
            });
        }
    }
}