using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Shop.DataBase;
using Microsoft.AspNetCore.Mvc;
using Shop.MiddleWare;
using NUnit.Framework;
using Shop.Repositories.ModelRepositories;
using Shop.Services.Contracts;
using Shop.Services.ModelServices;
using Shop.API.Controllers.V1;
using Mapper = Shop.MiddleWare.Mapper;

namespace Shop.API.Tests.Controllers
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
                mc.AddProfile(new Mapper());
            });
            var mapper = mapperConfig.CreateMapper();

            _brandService = new BrandService(new BrandRepository(_context), mapper);

            _controller = new BrandController(_brandService);
        }

        [Test, Order(1)]
        public async Task HttpGet_GetAll()
        {
            var actionResult = await _controller.GetAll();
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
            var actionResult = await _controller.GetById(1);
            
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
            Assert.That(() => _controller.GetById(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 999"));
        }

        [Test, Order(4)]
        public async Task HttpPost_AddBrand()
        {
            var allResBefore = await _controller.GetAll();
            var allResDataBefore = (allResBefore as OkObjectResult).Value as List<Brand>;
            
            var actionResult = await _controller.Add(new BrandVM()
            {
                Name = "Test"
            });

            var responseData = (actionResult as OkObjectResult).Value as Brand;
            
            
            var allResAfter = await _controller.GetAll();
            var allResDataAfter = (allResAfter as OkObjectResult).Value as List<Brand>;

            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual(allResDataBefore.Count+1, allResDataAfter.Count);
            
            Assert.AreEqual("Test",responseData.Name);
        }

        [Test, Order(5)]
        public async Task HttpPost_AddBrand_Err_AlreadyExists()
        {
            var allResBefore = await _controller.GetAll();
            var allResData = (allResBefore as OkObjectResult).Value as List<Brand>;

            Assert.That(() => _controller.Add(new BrandVM() { Name = "Nike" }), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Same Brand already exists"));


            var allResAfter = await _controller.GetAll();
            var allResDataAfter = (allResAfter as OkObjectResult).Value as List<Brand>;
            Assert.AreEqual(allResData.Count, allResDataAfter.Count);
        }

        [Test, Order(5)]
        public async Task HttpPost_AddBrand_Err_NoWM()
        {
            var allResBefore = await _controller.GetAll();
            var allResData = (allResBefore as OkObjectResult).Value as List<Brand>;

            var actionResult = await _controller.Add(null);
            
            var allResAfter = await _controller.GetAll();
            var allResDataAfter = (allResAfter as OkObjectResult).Value as List<Brand>;
            
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());

            var responseData = (actionResult as BadRequestObjectResult).Value as string;
            Assert.AreEqual("Null entity", responseData);
            
            Assert.AreEqual(allResData.Count, allResDataAfter.Count);
        }

        [Test, Order(6)]
        public async Task HttpPut_UpdateBrand()
        {
            var brandTypesCountBeforeUpdate = _context.BrandTypes.Count(bt => bt.BrandId == 1);
            var goods = _context.Goods.Where(g=>g.BrandId==1).ToList();
            var goodsCountBeforeUpdate = _context.Goods.Count(bt => bt.BrandId == 1);

            var actionResult = await _controller.Update(new BrandVM()
            {
                Name = "TestNew"
            }, 1);
            
            var currentBrand = await _controller.GetById(1);
            var currentBrandData = (currentBrand as OkObjectResult).Value as Brand;
            
            Assert.That(actionResult, Is.TypeOf<OkResult>());
            Assert.AreEqual(currentBrandData.Name, "TestNew");

            Assert.AreEqual( brandTypesCountBeforeUpdate, _context.BrandTypes.Count(bt => bt.BrandId == 1));
            Assert.AreEqual(goodsCountBeforeUpdate, _context.Goods.Count(bt => bt.BrandId == 1));
        }

        [Test, Order(7)]
        public async Task HttpPut_UpdateBrand_Err_WoVM()
        {
            var prevBrand = (await _controller.GetById(1) as OkObjectResult).Value as Brand;
            
            var actionResult = await _controller.Update(null, 1);
            
            var currentBrand = await _controller.GetById(1);
            var currentBrandData = (currentBrand as OkObjectResult).Value as Brand;
            
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());

            var responseData = (actionResult as BadRequestObjectResult).Value as string;
            Assert.AreEqual("Null entity", responseData);
            Assert.AreEqual(currentBrandData.Name, prevBrand.Name);
        }
        
        [Test, Order(8)]
        public void HttpPut_UpdateBrand_Err_WVm_WithOutExistingId()
        {   
            Assert.That(() => _controller.Update(new BrandVM() { Name = "Test1" }, 999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 999"));
        }
        
        [Test, Order(8)]
        public async Task HttpPut_UpdateBrand_Err_WVm_AlreadyExists()
        {
            var prevBrand = (await _controller.GetById(1) as OkObjectResult).Value as Brand;


            Assert.That(() => _controller.Update(new BrandVM() { Name = "Puma" }, 1), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Same Brand already exists"));


            var newBrand = (await _controller.GetById(1) as OkObjectResult).Value as Brand;
            
            Assert.AreEqual(prevBrand.Name, newBrand.Name);
        }
        
        [Test, Order(8)]
        public async Task HttpDelete_DeleteBrand()
        {
            var allResBefore = await _controller.GetAll();
            var allResData = (allResBefore as OkObjectResult).Value as List<Brand>;

            var actionResult =  await _controller.Delete(1);
            
            var allResAfter = await _controller.GetAll();
            var allResDataAfter = (allResAfter as OkObjectResult).Value as List<Brand>;
            
            Assert.That(actionResult, Is.TypeOf<OkResult>());
            
            Assert.AreEqual(allResData.Count-1, allResDataAfter.Count);

            Assert.AreEqual(0, _context.BrandTypes.Count(bt => bt.BrandId == 1));
            Assert.AreEqual(0, _context.Goods.Count(bt => bt.BrandId == 1));
        }
        
        [Test, Order(9)]
        public void HttpDeleteBrand_WithOut_ExistingId()
        {
            Assert.That(() => _controller.Delete(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Brand with id: 999"));
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}