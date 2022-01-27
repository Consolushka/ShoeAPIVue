using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebApplication.Controllers.V1;
using WebApplication.Data;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;
using WebApplication.Middleware;
using WebApplication.Repository.EntityRepository;
using WebApplication.Services.Contracts;
using WebApplication.Services.ModelServices;

namespace ShoeAPI_Tests.Controllers
{
    public class BrandControllerTests
    {
        private static DbContextOptions<ShopContext> _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;

        private ShopContext _context;

        private IBrandService _brandService;
        private BrandController _controller;

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

            _brandService = new BrandService(new BrandRepository(_context), mapper);

            _controller = new BrandController(_brandService);
        }

        [Test, Order(1)]
        public async Task HttpGet_GetAll()
        {
            IActionResult actionResult = await _controller.GetAll();
            var actionResultData = (actionResult as OkObjectResult).Value as List<Brand>;

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual(actionResultData.First().Name, "Nike");
        }
        
        [Test, Order(2)]
        public async Task HttpGet_GetById()
        {
            IActionResult actionResult = await _controller.GetById(1);
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionResultData = (actionResult as OkObjectResult).Value as Brand;
            
            Assert.AreEqual(actionResultData.Id, 1);
            Assert.AreEqual(actionResultData.Name, "Nike");
        }
        
        [Test, Order(3)]
        public void HttpGet_GetById_Error()
        {
            Assert.That(()=>_controller.GetById(3), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 3"));
        }

        [Test, Order(4)]
        public async Task HttpPost_AddBrand()
        {
            IActionResult actionResult = await _controller.Add(new BrandVM()
            {
                Name = "Test"
            });
            
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [Test, Order(5)]
        public async Task HttpPost_AddBrand_Err()
        {
            IActionResult actionResult = await _controller.Add(null);
            
            Assert.That(actionResult, Is.TypeOf<BadRequestResult>());
        }

        [Test, Order(6)]
        public async Task HttpPut_UpdateBrand()
        {
            IActionResult actionResult = await _controller.Update(new BrandVM()
            {
                Name = "Test"
            }, 1);
            
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [Test, Order(7)]
        public async Task HttpPut_UpdateBrand_Err_WoVM()
        {
            IActionResult actionResult = await _controller.Update(null, 1);
            
            Assert.That(actionResult, Is.TypeOf<BadRequestResult>());
        }
        
        [Test, Order(8)]
        public void HttpPut_UpdateBrand_Err_WVm_WithOutExistingId()
        {   
            Assert.That(()=>_controller.Update(new BrandVM() { Name = "Test1" }, 999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 999"));
        }
        
        [Test, Order(8)]
        public async Task HttpDelete_DeleteBrand()
        {
            var actionResult =  await _controller.Delete(1);
            
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }
        
        [Test, Order(9)]
        public void HttpDeleteBrand_WithErr()
        {
            Assert.That(()=>_controller.Delete(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 999"));
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
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
                    Address = "Listvenichnaya alleya 2A",
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