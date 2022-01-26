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
    public class ShoeServiceTests
    {
        private static DbContextOptions<ShoeContext> _dbContextOptions = new DbContextOptionsBuilder<ShoeContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;

        private ShoeContext _context;

        private IShoeService _shoeService;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new ShoeContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BrandMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _shoeService = new ShoeService(new ShoeRepository(_context), mapper);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetAll()
        {
            IEnumerable<Shoe> res = await _shoeService.GetAll();
            
            Assert.AreEqual(res.Count(), 2);
        }

        [Test]
        public async Task GetById_Success()
        {
            Shoe res = await _shoeService.GetById(1);
            
            Assert.AreEqual(1,res.Id);
        }
        
        [Test]
        public void GetById_Err()
        {
            Assert.That(()=>_shoeService.GetById(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Shoe with id: 999"));
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