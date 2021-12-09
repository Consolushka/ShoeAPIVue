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
    public class ShoeControllerTests
    {
        private static DbContextOptions<ShoeContext> _dbContextOptions = new DbContextOptionsBuilder<ShoeContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;

        private readonly ShoeContext _context = new (_dbContextOptions);

        private IShoeService ShoeService;
        private ShoeController ShoeController;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context.Database.EnsureCreated();

            SeedDatabase();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ShoeMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            ShoeService = new ShoeService(new ShoeRepository(_context), mapper);

            ShoeController = new ShoeController(ShoeService);
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void HttpGet_GetAll()
        {
            IActionResult actionResult = ShoeController.GetAll();
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionDate = (actionResult as OkObjectResult).Value as List<Shoe>;
            Assert.AreEqual(actionDate.First().Name, "Nike v.1");
        }

        [Test, Order(2)]
        public void HttpGet_GetById_Success()
        {
            var res = ShoeController.GetById(1);
            
            Assert.That(res, Is.TypeOf<OkObjectResult>());
            var actionDate = (res as OkObjectResult).Value as Shoe;
            Assert.AreEqual(actionDate.Name, "Nike v.1");
        }

        [Test, Order(3)]
        public void HttpGet_GetById_Err()
        {
            Assert.That(()=>ShoeController.GetById(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Shoe with id: 999"));
        }

        [Test, Order(4)]
        public void HttpPost_AddShoe_Success()
        {
            var res = ShoeController.Add(new ShoeVM()
            {
                Name = "Test",
                BrandId = 1,
                CreationTime = new DateTime(),
                PhotoFileName = "undefined.png"
            });
            
            Assert.That(res, Is.TypeOf<OkResult>());
        }

        [Test, Order(5)]
        public void HttpPost_AddShoe_Err()
        {
            var res = ShoeController.Add(null);
            
            Assert.That(res, Is.TypeOf<BadRequestResult>());
        }

        [Test, Order(6)]
        public async Task HttpPu_Update_Success()
        {
            var res = await ShoeController.Update(new ShoeVM()
            {
                BrandId = 2,
                Name = "Test",
                CreationTime = new DateTime(),
                PhotoFileName = "undefined1.png"
            }, 1);
            
            Assert.That(res, Is.TypeOf<OkObjectResult>());

            var resData = (res as OkObjectResult).Value as Shoe;
            
            Assert.AreEqual(1, resData.Id);
            Assert.AreEqual(2, resData.BrandId);
            Assert.AreEqual("Test", resData.Name);
            Assert.AreEqual(new DateTime(), resData.CreationTime);
            Assert.AreEqual("undefined1.png", resData.PhotoFileName);
        }

        [Test, Order(7)]
        public async Task HttpPut_Update_WithoutVM()
        {
            var res = await ShoeController.Update(null, 1);
            
            Assert.That(res, Is.TypeOf<BadRequestResult>());
        }
        
        [Test, Order(8)]
        public void HttpPut_Update_WithoutId()
        {
            
            Assert.That(()=>ShoeController.Update(new ShoeVM()
            {
                BrandId = 2,
                Name = "Test",
                CreationTime = new DateTime(),
                PhotoFileName = "undefined1.png"
            }, 999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Shoe with id: 999"));
        }

        [Test, Order(9)]
        public void HttpDelete_Delete_Success()
        {
            var res = ShoeController.Delete(1);
            
            Assert.That(res, Is.TypeOf<OkResult>());
        }

        [Test, Order(10)]
        public void HttpDelete_Delete_Err()
        {
            Assert.That(()=>ShoeController.Delete(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Shoe with id: 999"));
        }
        
        private void SeedDatabase()
        {
            SeedUsersIfNone(_context);
            SeedBrandsIfNone(_context);
            SeedShoesIfNone(_context);

            _context.SaveChanges();
        }
        
        private static void SeedUsersIfNone(ShoeContext context)
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
        
        private static void SeedBrandsIfNone(ShoeContext context)
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
        
        private static void SeedShoesIfNone(ShoeContext context)
        {
            if (!context.Shoes.Any())
            {
                context.Shoes.AddRange(new List<Shoe>()
                {
                    new Shoe()
                    {
                        Name = "Nike v.1",
                        BrandId = 1,
                        CreationTime = DateTime.Now,
                        PhotoFileName = "undefined.jpg"
                    },
                    new Shoe()
                    {
                        Name = "Puma v.1",
                        BrandId = 2,
                        CreationTime = DateTime.Now.AddDays(-1),
                        PhotoFileName = "undefined.jpg"
                    },
                });
            }
        }
    }
}