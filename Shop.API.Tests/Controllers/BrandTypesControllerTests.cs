using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Data.Models;
using Shop.DataBase;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Shop.Repositories.ModelRepositories;
using Shop.Services.Contracts;
using Shop.Services.ModelServices;
using Shop.API.Controllers.V1;
using Mapper = Shop.MiddleWare.Mapper;
using Type = Shop.Data.Models.Type;

namespace Shop.API.Tests.Controllers
{
    public class BrandTypesControllerTests
    {
        private ShopContext _context;

        private IBrandTypeService _brandService;
        private BrandTypesController _controller;

        [OneTimeSetUp]
        public void Setup()
        {

            _context = DBSeeding.CreateDb();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _brandService = new BrandTypeService(new BrandTypeRepository(_context));

            _controller = new BrandTypesController(_brandService);
        }

        [Test, Order(1)]
        public async Task HttpGet_GetAll()
        {
            IActionResult actionResult = await _controller.GetAll();
            var actionResultData = (actionResult as OkObjectResult).Value as List<BrandType>;

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual(3, actionResultData.Count);
            
            Assert.AreEqual("Nike", actionResultData.Where(bt=>bt.Id==1).First().Brand.Name);
            Assert.AreEqual("Shoe", actionResultData.Where(bt=>bt.Id==1).First().Type.Name);
            
            Assert.AreEqual("Nike", actionResultData.Where(bt=>bt.Id==2).First().Brand.Name);
            Assert.AreEqual("T-Shirt", actionResultData.Where(bt=>bt.Id==2).First().Type.Name);
            
            Assert.AreEqual("Puma", actionResultData.Where(bt=>bt.Id==3).First().Brand.Name);
            Assert.AreEqual("Shoe", actionResultData.Where(bt=>bt.Id==3).First().Type.Name);
        }
        
        [Test, Order(2)]
        public async Task HttpGet_GetAllByBrand()
        {
            IActionResult actionResult = await _controller.GetAllByBrand(1);
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionResultData = (actionResult as OkObjectResult).Value as List<Type>;
            var res = actionResultData.OrderBy(bt => bt.Id).ToList();
            
            Assert.AreEqual(2,res.Count);
            
            Assert.AreEqual(1, res[0].Id);
            Assert.AreEqual(2,res[1].Id);
        }
        
        [Test, Order(4)]
        public async Task HttpGet_GetAllByType()
        {
            IActionResult actionResult = await _controller.GetAllByType(1);
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionResultData = (actionResult as OkObjectResult).Value as List<Brand>;
            var res = actionResultData.OrderBy(bt => bt.Id).ToList();
            
            Assert.AreEqual(2,res.Count);
            
            Assert.AreEqual(1, res[0].Id);
            Assert.AreEqual(2,res[1].Id);
        }
        
        [Test, Order(6)]
        public async Task HttpPost_AddBrandType_Success()
        {
            IActionResult allBeforeAdding = await _controller.GetAll();
            IActionResult actionResult = await _controller.Add(new BrandType()
            {
                TypeId = 2,
                BrandId = 2
            });
            IActionResult allAfterAdding = await _controller.GetAll();
            var allAfterRes = ((allAfterAdding as OkObjectResult).Value as List<BrandType>).OrderBy(bt => bt.Id).ToList();
            var allBeforeRes = ((allBeforeAdding as OkObjectResult).Value as List<BrandType>).OrderBy(bt => bt.Id).ToList();
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual(allBeforeRes.Count+1, allAfterRes.Count);
            Assert.AreEqual(2, allAfterRes[3].BrandId);
            Assert.AreEqual(2, allAfterRes[3].TypeId);
        }
        
        [Test, Order(7)]
        public async Task HttpPost_AddBrandType_Err_AlreadyExists()
        {
            IActionResult allBeforeAdding = await _controller.GetAll();

            var res = (await _controller.Add(new BrandType() { TypeId = 1, BrandId = 1 }) as BadRequestObjectResult)
                .Value as Exception;
            
            Assert.AreEqual("Same BrandType already exists", res.Message);
            
            IActionResult allAfterAdding = await _controller.GetAll();
            var allAfterRes = (allAfterAdding as OkObjectResult).Value as List<BrandType>;
            var allBeforeRes = (allBeforeAdding as OkObjectResult).Value as List<BrandType>;
            
            Assert.AreEqual(allBeforeRes.Count, allAfterRes.Count);
        }
        
        
        // [Test, Order(4)]
        // public async Task HttpPost_AddBrand()
        // {
        //     IActionResult actionResult = await _controller.Add(new BrandVM()
        //     {
        //         Name = "Test"
        //     });
        //
        //     IActionResult allRes = await _controller.GetAll();
        //     var allResData = (allRes as OkObjectResult).Value as List<Brand>;
        //     
        //     Assert.That(actionResult, Is.TypeOf<OkResult>());
        //     Assert.AreEqual(allResData.Count, 3);
        //     Assert.AreEqual("Test", allResData[2].Name);
        // }
        //
        // [Test, Order(5)]
        // public async Task HttpPost_AddBrand_Err()
        // {
        //     IActionResult actionResult = await _controller.Add(null);
        //     IActionResult allRes = await _controller.GetAll();
        //     var allResData = (allRes as OkObjectResult).Value as List<Brand>;
        //     
        //     Assert.That(actionResult, Is.TypeOf<BadRequestResult>());
        //     Assert.AreEqual(allResData.Count, 3);
        // }
        //
        // [Test, Order(6)]
        // public async Task HttpPut_UpdateBrand()
        // {
        //     IActionResult actionResult = await _controller.Update(new BrandVM()
        //     {
        //         Name = "Test"
        //     }, 1);
        //     
        //     IActionResult currBrand = await _controller.GetById(1);
        //     var currBrandData = (currBrand as OkObjectResult).Value as Brand;
        //     
        //     Assert.That(actionResult, Is.TypeOf<OkResult>());
        //     Assert.AreEqual(currBrandData.Name, "Test");
        // }
        //
        // [Test, Order(7)]
        // public async Task HttpPut_UpdateBrand_Err_WoVM()
        // {
        //     IActionResult actionResult = await _controller.Update(null, 1);
        //     
        //     IActionResult currBrand = await _controller.GetById(1);
        //     var currBrandData = (currBrand as OkObjectResult).Value as Brand;
        //     
        //     Assert.That(actionResult, Is.TypeOf<BadRequestResult>());
        //     Assert.AreEqual(currBrandData.Name, "Test");
        // }
        //
        // [Test, Order(8)]
        // public void HttpPut_UpdateBrand_Err_WVm_WithOutExistingId()
        // {   
        //     Assert.That(()=>_controller.Update(new BrandVM() { Name = "Test1" }, 999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 999"));
        // }
        //
        // [Test, Order(8)]
        // public async Task HttpDelete_DeleteBrand()
        // {
        //     var actionResult =  await _controller.Delete(1);
        //     
        //     IActionResult allRes = await _controller.GetAll();
        //     var allResData = (allRes as OkObjectResult).Value as List<Brand>;
        //     
        //     Assert.That(actionResult, Is.TypeOf<OkResult>());
        //     
        //     Assert.AreEqual(allResData.Count, 2);
        // }
        //
        // [Test, Order(9)]
        // public void HttpDeleteBrand_WithOut_ExistingId()
        // {
        //     Assert.That(()=>_controller.Delete(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 999"));
        // }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}