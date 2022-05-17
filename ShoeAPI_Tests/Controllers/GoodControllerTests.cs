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
    public class GoodControllerTests
    {
        private static DbContextOptions<ShopContext> _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;

        private ShopContext _context;

        private IGoodsService _goodsService;
        private GoodsController _controller;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context = DBSeeding.CreateDb();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GoodMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _goodsService = new GoodsService(new GoodsRepository(_context), mapper);

            _controller = new GoodsController(_goodsService, null);
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public async Task HttpGet_GetAll()
        {
            IActionResult actionResult = await _controller.GetAll();
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionDate = (actionResult as OkObjectResult).Value as List<Good>;
            Assert.AreEqual(3, actionDate.Count);
        }

        [Test, Order(2)]
        public async Task HttpGet_GetById_Success()
        {
            var res = await _controller.GetById(1);
            
            Assert.That(res, Is.TypeOf<OkObjectResult>());
            var actionDate = (res as OkObjectResult).Value as Good;
            Assert.AreEqual("Nike v.1", actionDate.Name);
        }

        [Test, Order(3)]
        public void HttpGet_GetById_Err()
        {
            Assert.That(()=> _controller.GetById(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Good with id: 999"));
        }

        [Test, Order(4)]
        public async Task HttpPost_AddGood_Success()
        {   
            var beforeData = (await _controller.GetAll() as OkObjectResult).Value as List<Good>;
            
            var res = await _controller.Add(new GoodVm()
            {
                Name = "Test",
                BrandId = 1,
                TypeId = 1,
                PhotoFileName = "undefined.png"
            });
            var afterData = ((await _controller.GetAll() as OkObjectResult).Value as List<Good>).OrderBy(g=>g.Id).ToList();
            
            Assert.That(res, Is.TypeOf<OkResult>());
            
            Assert.AreEqual(beforeData.Count+1, afterData.Count);
            Assert.AreEqual("Test", afterData.Last().Name);
        }

        [Test, Order(5)]
        public async Task HttpPost_AddGood_Err_NoVM()
        {
            var beforeData = (await _controller.GetAll() as OkObjectResult).Value as List<Good>;
            
            var res = await _controller.Add(null);
            
            var afterData = ((await _controller.GetAll() as OkObjectResult).Value as List<Good>).OrderBy(g=>g.Id).ToList();
            
            Assert.That(res, Is.TypeOf<BadRequestResult>());
            
            Assert.AreEqual(beforeData.Count, afterData.Count);
        }

        [Test, Order(5)]
        public async Task HttpPost_AddGood_Err_Exists()
        {   
            var beforeData = (await _controller.GetAll() as OkObjectResult).Value as List<Good>; 
            
            Assert.That(async ()=>await _controller.Add(new GoodVm(){
                Name = "Nike v.1",
                BrandId = 1,
                TypeId = 1,
                PhotoFileName = "undefined.png"
            }), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Same Good already exists"));
            
            var afterData = ((await _controller.GetAll() as OkObjectResult).Value as List<Good>).OrderBy(g=>g.Id).ToList();
            
            Assert.AreEqual(beforeData.Count, afterData.Count);
        }

        [Test, Order(6)]
        public async Task HttpPu_Update_Success()
        {
            var res = await _controller.Update(new GoodVm()
            {
                BrandId = 2,
                TypeId = 2,
                Name = "Test",
                PhotoFileName = "undefined1.png"
            }, 3);
            
            Assert.That(res, Is.TypeOf<OkObjectResult>());

            var resData = (res as OkObjectResult).Value as Good;
            
            Assert.AreEqual(3, resData.Id);
            Assert.AreEqual(2, resData.BrandId);
            Assert.AreEqual(2, resData.TypeId);
            Assert.AreEqual("Test", resData.Name);
            Assert.AreEqual("undefined1.png", resData.PhotoFileName);
        }

        [Test, Order(7)]
        public async Task HttpPut_Update_WithoutVM()
        {
            var res = await _controller.Update(null, 1);
            
            Assert.That(res, Is.TypeOf<BadRequestResult>());
        }
        
        [Test, Order(8)]
        public void HttpPut_Update_WithoutId()
        {
            
            Assert.That(()=>_controller.Update(new GoodVm()
            {
                BrandId = 2,
                Name = "Test",
                PhotoFileName = "undefined1.png"
            }, 999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Good with id: 999"));
        }
        
        [Test, Order(8)]
        public void HttpPut_Update_Err_Existing()
        {
            
            Assert.That(()=>_controller.Update(new GoodVm()
            {
                TypeId = 1,
                BrandId = 1,
                Name = "Nike v.1",
                PhotoFileName = "undefined1.png"
            }, 3), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Same Good already exists"));
        }

        [Test, Order(9)]
        public async Task HttpDelete_Delete_Success()
        {
            var res = await _controller.Delete(1);
            
            Assert.That(res, Is.TypeOf<OkResult>());
        }

        [Test, Order(10)]
        public void HttpDelete_Delete_Err()
        {
            Assert.That(()=>_controller.Delete(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Good with id: 999"));
        }
    }
}