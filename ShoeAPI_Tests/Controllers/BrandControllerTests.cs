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
        private ShopContext _context;

        private IBrandService _brandService;
        private BrandController _controller;

        [OneTimeSetUp]
        public void Setup()
        {

            _context = DBSeeding.CreateDb();
            
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
            Assert.AreEqual("Nike", actionResultData.Find(b=>b.Id==1).Name);
            Assert.AreEqual(2,actionResultData.Find(b=>b.Id==1).Types.Count);
            Assert.AreEqual(2,actionResultData.Find(b=>b.Id==1).Goods.Count);
            
            Assert.AreEqual("Puma", actionResultData.Find(b=>b.Id==2).Name);
            Assert.AreEqual(1,actionResultData.Find(b=>b.Id==2).Types.Count);
            Assert.AreEqual(1,actionResultData.Find(b=>b.Id==2).Goods.Count);
            
            Assert.AreEqual(2, actionResultData.Count);
        }
        
        [Test, Order(2)]
        public async Task HttpGet_GetById()
        {
            IActionResult actionResult = await _controller.GetById(1);
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionResultData = (actionResult as OkObjectResult).Value as Brand;
            
            Assert.AreEqual(1, actionResultData.Id);
            Assert.AreEqual("Nike",actionResultData.Name);
            Assert.AreEqual(2,actionResultData.Types.Count);
            Assert.AreEqual(2,actionResultData.Goods.Count);
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

            IActionResult allRes = await _controller.GetAll();
            var allResData = (allRes as OkObjectResult).Value as List<Brand>;
            
            Assert.That(actionResult, Is.TypeOf<OkResult>());
            Assert.AreEqual(allResData.Count, 3);
        }

        [Test, Order(5)]
        public async Task HttpPost_AddBrand_Err()
        {
            IActionResult actionResult = await _controller.Add(null);
            IActionResult allRes = await _controller.GetAll();
            var allResData = (allRes as OkObjectResult).Value as List<Brand>;
            
            Assert.That(actionResult, Is.TypeOf<BadRequestResult>());
            Assert.AreEqual(allResData.Count, 3);
        }

        [Test, Order(6)]
        public async Task HttpPut_UpdateBrand()
        {
            IActionResult actionResult = await _controller.Update(new BrandVM()
            {
                Name = "Test"
            }, 1);
            
            IActionResult currBrand = await _controller.GetById(1);
            var currBrandData = (currBrand as OkObjectResult).Value as Brand;
            
            Assert.That(actionResult, Is.TypeOf<OkResult>());
            Assert.AreEqual(currBrandData.Name, "Test");
        }

        [Test, Order(7)]
        public async Task HttpPut_UpdateBrand_Err_WoVM()
        {
            IActionResult actionResult = await _controller.Update(null, 1);
            
            IActionResult currBrand = await _controller.GetById(1);
            var currBrandData = (currBrand as OkObjectResult).Value as Brand;
            
            Assert.That(actionResult, Is.TypeOf<BadRequestResult>());
            Assert.AreEqual(currBrandData.Name, "Test");
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
            
            IActionResult allRes = await _controller.GetAll();
            var allResData = (allRes as OkObjectResult).Value as List<Brand>;
            
            Assert.That(actionResult, Is.TypeOf<OkResult>());
            
            Assert.AreEqual(allResData.Count, 2);
        }
        
        [Test, Order(9)]
        public void HttpDeleteBrand_WithOut_ExistingId()
        {
            Assert.That(()=>_controller.Delete(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 999"));
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}