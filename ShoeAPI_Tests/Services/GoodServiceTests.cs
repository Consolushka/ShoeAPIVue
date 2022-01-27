using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Middleware;
using WebApplication.Repository.EntityRepository;
using WebApplication.Services.Contracts;
using WebApplication.Services.ModelServices;

namespace ShoeAPI_Tests.Services
{
    public class GoodServiceTests
    {
        private static DbContextOptions<ShopContext> _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;

        private ShopContext _context;

        private IGoodsService _goodsService;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new ShopContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BrandMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _goodsService = new GoodsService(new GoodsRepository(_context), mapper);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetAll()
        {
            IEnumerable<Good> res = await _goodsService.GetAll();
            
            Assert.AreEqual(res.Count(), 2);
        }

        [Test]
        public async Task GetById_Success()
        {
            Good res = await _goodsService.GetById(1);
            
            Assert.AreEqual(1,res.Id);
        }
        
        [Test]
        public void GetById_Err()
        {
            Assert.That(()=>_goodsService.GetById(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Good with id: 999"));
        }
        

        private void SeedDatabase()
        {
            SeedUsersIfNone(_context);
            SeedBrandsIfNone(_context);
            SeedGoodsIfNone(_context);

            _context.SaveChanges();
        }
        
        private static void SeedUsersIfNone(ShopContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User()
                {
                    ConfirmString = new Guid(),
                    Email = "consolushka@gmail.com",
                    Password = "CgwJ4C/o1BOl1hyEtdcTwg==",
                    IsActive = true,
                    UserName = "admin"
                });
            }
        }
        
        private static void SeedBrandsIfNone(ShopContext context)
        {
            if (!context.Brands.Any())
            {
                context.Brands.AddRange(new List<Brand>()
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
        }
        
        private static void SeedGoodsIfNone(ShopContext context)
        {
            if (!context.Goods.Any())
            {
                context.Goods.AddRange(new List<Good>()
                {
                    new Good()
                    {
                        Name = "Nike v.1",
                        BrandId = 1,
                        PhotoFileName = "undefined.jpg"
                    },
                    new Good()
                    {
                        Name = "Puma v.1",
                        BrandId = 2,
                        PhotoFileName = "undefined.jpg"
                    },
                });
            }
        }
    }
}