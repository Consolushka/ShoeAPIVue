using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Shop.DataBase;
using Microsoft.AspNetCore.Mvc;
using Shop.MiddleWare;
using NUnit.Framework;
using Scrutor;
using Shop.Repositories.ModelRepositories;
using Shop.Services.Contracts;
using Shop.Services.ModelServices;
using Shop.API.Controllers.V1;
using Mapper = Shop.MiddleWare.Mapper;

namespace Shop.API.Tests.Controllers
{
    public class OrderControllerTests
    {
        private ShopContext _context;

        private IOrderService _service;
        private OrderController _controller;

        [OneTimeSetUp]
        public void Setup()
        {

            _context = DBSeeding.CreateDb();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _service = new OrderService(new OrderRepository(_context), new OrderItemRepository(_context), new StockItemService(new StockItemRepository(_context)));

            _controller = new OrderController(_service);
        }

        [Test, Order(1)]
        public async Task HttpGet_GetAll()
        {
            IActionResult actionResult = await _controller.GetAll();
            var actionResultData = (actionResult as OkObjectResult).Value as List<Order>;

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            Assert.AreEqual(1, actionResultData.Count);
        }
        
        [Test, Order(2)]
        public async Task HttpGet_GetById()
        {
            IActionResult actionResult = await _controller.GetById(1);
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionResultData = (actionResult as OkObjectResult).Value as Order;
            
            Assert.AreEqual(1, actionResultData.Id);
            Assert.AreEqual(1,actionResultData.UserId);
            Assert.AreEqual(1,actionResultData.Status);
            Assert.AreEqual(false,actionResultData.IsPaid);
        }
        
        [Test, Order(3)]
        public async Task HttpGet_GetById_Error()
        {
            IActionResult actionResult = await _controller.GetById(999);
            
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());

            var responseData = (actionResult as BadRequestObjectResult).Value as Exception;
            
            Assert.AreEqual("Cannot find Order with id: 999", responseData.Message);
        }
        
        [Test, Order(2)]
        public async Task HttpGet_GetByUser()
        {
            IActionResult actionResult = await _controller.GetByUser(1);
            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            
            var actionResultData = (actionResult as OkObjectResult).Value as List<Order>;
            
            Assert.AreEqual(1, actionResultData.Count);
        }

        [Test, Order(4)]
        public async Task HttpPost_AddOrder_Success()
        {
            IActionResult allResBefore = await _controller.GetByUser(1);
            var allResDataBefore = (allResBefore as OkObjectResult).Value as List<Order>;
            
            IActionResult actionResult = await _controller.Add(new Order()
            {
                Status = 1,
                UserId = 1,
                IsPaid = false,
                OrderTime = DateTime.Now.AddDays(-2)
            });

            var responseData = (actionResult as OkObjectResult).Value as Order;
            
            
            IActionResult allResAfter = await _controller.GetByUser(1);
            var allResDataAfter = (allResAfter as OkObjectResult).Value as List<Order>;

            
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual(allResDataBefore.Count+1, allResDataAfter.Count);
            
            
            Assert.AreEqual(2, responseData.Id);
            Assert.AreEqual(1,responseData.UserId);
            Assert.AreEqual(1,responseData.Status);
            Assert.AreEqual(false,responseData.IsPaid);
        }

        [Test, Order(5)]
        public async Task HttpPost_AddOrder_Err_NoWM()
        {
            IActionResult allResBefore = await _controller.GetAll();
            var allResData = (allResBefore as OkObjectResult).Value as List<Order>;

            IActionResult actionResult = await _controller.Add(null);
            
            IActionResult allResAfter = await _controller.GetAll();
            var allResDataAfter = (allResAfter as OkObjectResult).Value as List<Order>;
            
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());

            var responseData = (actionResult as BadRequestObjectResult).Value as string;
            Assert.AreEqual("Null entity", responseData);
            
            Assert.AreEqual(allResData.Count, allResDataAfter.Count);
        }

        [Test, Order(6)]
        public async Task HttpPut_UpdateStatus()
        {
            var res = await _controller.UpdateStatus(2, 1);
            
            Assert.That(res, Is.TypeOf<OkResult>());
        }

        [Test, Order(7)]
        public async Task HttpPut_UpdateStatus_NoStatus()
        {

            var res = (await _controller.UpdateStatus(0, 1) as BadRequestObjectResult).Value as string;
            
            Assert.AreEqual("Null status", res);
        }

        [Test, Order(8)]
        public async Task HttpPut_UpdateStatus_NoId()
        {

            var res = (await _controller.UpdateStatus(2, 999) as BadRequestObjectResult).Value as Exception;
            
            Assert.AreEqual("Cannot find Order with id: 999", res.Message);
        }

        // [Test, Order(6)]
        // public async Task HttpPut_UpdateBrand()
        // {
        //     IActionResult actionResult = await _controller.Update(new BrandVM()
        //     {
        //         Name = "TestNew"
        //     }, 1);
        //     
        //     IActionResult currBrand = await _controller.GetById(1);
        //     var currBrandData = (currBrand as OkObjectResult).Value as Brand;
        //     
        //     Assert.That(actionResult, Is.TypeOf<OkResult>());
        //     Assert.AreEqual(currBrandData.Name, "TestNew");
        // }
        //
        // [Test, Order(7)]
        // public async Task HttpPut_UpdateBrand_Err_WoVM()
        // {
        //     var prevBrand = (await _controller.GetById(1) as OkObjectResult).Value as Brand;
        //     
        //     IActionResult actionResult = await _controller.Update(null, 1);
        //     
        //     IActionResult currBrand = await _controller.GetById(1);
        //     var currBrandData = (currBrand as OkObjectResult).Value as Brand;
        //     
        //     Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        //
        //     var responseData = (actionResult as BadRequestObjectResult).Value as string;
        //     Assert.AreEqual("Null entity", responseData);
        //     Assert.AreEqual(currBrandData.Name, prevBrand.Name);
        // }
        //
        // [Test, Order(8)]
        // public async Task HttpPut_UpdateBrand_Err_WVm_WithOutExistingId()
        // {   
        //     IActionResult actionResult = await _controller.Update(new BrandVM() { Name = "Test1" }, 999);
        //     
        //     Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        //
        //     var responseData = (actionResult as BadRequestObjectResult).Value as Exception;
        //     
        //     Assert.AreEqual("Cannot find Brand with id: 999", responseData.Message);
        // }
        //
        // [Test, Order(8)]
        // public async Task HttpPut_UpdateBrand_Err_WVm_AlreadyExists()
        // {
        //     var prevBrand = (await _controller.GetById(1) as OkObjectResult).Value as Brand;
        //     
        //     IActionResult addedBrand = await _controller.Update(new BrandVM() { Name = "Puma" }, 1);
        //     var responseData = (addedBrand as BadRequestObjectResult).Value as Exception;
        //     
        //     var newBrand = (await _controller.GetById(1) as OkObjectResult).Value as Brand;
        //     
        //     Assert.AreEqual("Same Brand already exists", responseData.Message);
        //     
        //     Assert.AreEqual(prevBrand.Name, newBrand.Name);
        // }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}