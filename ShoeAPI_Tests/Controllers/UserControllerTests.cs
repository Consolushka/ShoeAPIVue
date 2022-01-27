using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public class UserControllerTests
    {
        private static DbContextOptions<ShopContext> _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;

        private ShopContext _context;

        private IUserService _userService;
        private UsersController _controller;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new ShopContext(_dbContextOptions);
            _context.Database.EnsureCreated();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            SeedDatabase();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _userService = new UserService(new UserRepository(_context), configuration, mapper);
            
            _controller = new UsersController(_userService);
        }

        [Test]
        public async Task HttpPost_Authenticate_Success()
        {
            IActionResult res = await _controller.Authenticate(new UserVM()
            {
                Email = "consolushka@gmail.com",
                Password = "admin",
                UserName = "admin"
            });
            
            Assert.That(res, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task HttpPost_Authenticate_IncorrectPasswordOrUser()
        {
            IActionResult res = await _controller.Authenticate(new UserVM()
            {
                Email = "consolushka@gmail.com",
                Password = "admin123",
                UserName = "admin"
            });
            
            Assert.That(res, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task HttpPost_Register_Err()
        {
            IActionResult res = await _controller.Register(new UserVM()
            {
                Email = "consolushka@gmail.com",
                Password = "admin123",
                UserName = "admin"
            });
            
            Assert.That(res, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task HttpGet_GetById()
        {
            IActionResult res = await _controller.GetById(1);
            
            Assert.That(res, Is.TypeOf<OkObjectResult>());

            var resData = (res as OkObjectResult).Value as UserResponse;
            
            Assert.AreEqual("consolushka@gmail.com",resData.Email);
            Assert.AreEqual(true, resData.IsActive);
            Assert.AreEqual(true, resData.IsAdmin);
            Assert.AreEqual("admin", resData.UserName);
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
                    IsAdmin = true,
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