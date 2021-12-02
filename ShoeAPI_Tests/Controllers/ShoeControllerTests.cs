using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebApplication.Controllers.V1;
using WebApplication.Data;
using WebApplication.Data.Models;
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

        private ShoeContext _context;

        private IShoeService _shoeService;
        private ShoeController _controller;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new ShoeContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ShoeMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _shoeService = new ShoeService(new ShoeRepository(_context), mapper);

            _controller = new ShoeController(_shoeService);
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void HttpGet_GetAll()
        {
            IActionResult actionResult = _controller.GetAll();
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionDate = (actionResult as OkObjectResult).Value as List<Shoe>;
            Assert.AreEqual(actionDate.First().Name, "Nike v.1");
        }

        [Test]
        public void HttpGet_GetById_Success()
        {
            var res = _controller.GetById(1);
            
            Assert.That(res, Is.TypeOf<OkObjectResult>());
            var actionDate = (res as OkObjectResult).Value as Shoe;
            Assert.AreEqual(actionDate.Name, "Nike v.1");
        }

        [Test]
        public void HttpGet_GetById_Err()
        {
            Assert.That(()=>_controller.GetById(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Shoe with id: 999"));
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