using System;
using System.Collections.Generic;
using System.Linq;
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
        private static DbContextOptions<ShoeContext> _dbContextOptions = new DbContextOptionsBuilder<ShoeContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;

        private ShoeContext _context;

        private IUserService _userService;
        private UsersController _controller;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new ShoeContext(_dbContextOptions);
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
        public void HttpPost_Authenticate_Success()
        {
            var res = _controller.Authenticate(new UserVM()
            {
                Email = "consolushka@gmail.com",
                Password = "admin",
                UserName = "admin"
            });
            
            Assert.That(res, Is.TypeOf<OkObjectResult>());
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