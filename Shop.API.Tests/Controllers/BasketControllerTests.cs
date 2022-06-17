using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Shop.API.Controllers.V1;
using Shop.Data.Models;
using Shop.DataBase;
using Shop.Repositories.ModelRepositories;
using Shop.Services.Contracts;
using Shop.Services.ModelServices;
using Mapper = Shop.MiddleWare.Mapper;

namespace Shop.API.Tests.Controllers;

public class BasketControllerTests
{
    private ShopContext _context;

    private BasketController _controller;

    [OneTimeSetUp]
    public void Setup()
    {

        _context = DBSeeding.CreateDb();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new Mapper());
        });

        BasketService BasketService = new (new BasketRepository(_context));
        BasketItemService BasketItemService = new (new BasketItemRepository(_context));


        _controller = new BasketController(BasketService, BasketItemService);
    }

    [Test, Order(1)]
    public async Task GetByUser_Success()
    {
        const long userId = 1;
        var result = await _controller.GetByUser(userId);

        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var resultDataBasket = (result as OkObjectResult).Value as Basket;

        Assert.AreEqual(userId,resultDataBasket.UserId);
        Assert.AreEqual(0, resultDataBasket.BasketItems.Count);
    }

    [Test, Order(1)]
    public void GetByUser_Failure()
    {
        const long userId = 999;

        Assert.That(()=>_controller.GetByUser(userId),Throws.Exception.TypeOf<Exception>().With.Message.EqualTo($"Cannot find User with id: {userId}"));
    }

    [Test, Order(2)]
    public async Task AddItem_Success()
    {
        BasketItem item = new BasketItem()
        {
            BasketId = 1,
            StockItemId = 1,
            Count = 1,
            Cost = 220.31
        };

        var result = await _controller.AddItem(item);

        Assert.That(result, Is.TypeOf<OkResult>());

        var userBasket = (await _controller.GetByUser(1) as OkObjectResult).Value as Basket;

        Assert.AreEqual(1,userBasket.BasketItems.Count);
        Assert.AreEqual(1,userBasket.BasketItems[0].StockItemId);
        Assert.AreEqual(1,userBasket.BasketItems[0].Count);
        Assert.AreEqual(220.31, userBasket.BasketItems[0].Cost);
    }

    [Test, Order(3)]
    public async Task UpdateQuantity_Success()
    {
        var result = await _controller.UpdateQuantity(1, 2);

        Assert.That(result, Is.TypeOf<OkResult>());

        var userBasket = (await _controller.GetByUser(1) as OkObjectResult).Value as Basket;

        Assert.AreEqual(1, userBasket.BasketItems.Count);
        Assert.AreEqual(1,userBasket.BasketItems[0].StockItemId);
        Assert.AreEqual(2,userBasket.BasketItems[0].Count);
        Assert.AreEqual(220.31, userBasket.BasketItems[0].Cost);
    }

    [Test, Order(4)]
    public async Task UpdateQuantity_Failure()
    {
        Assert.That(()=>_controller.UpdateQuantity(999,2), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find BasketItem with id: 999"));

        var userBasket = (await _controller.GetByUser(1) as OkObjectResult).Value as Basket;

        Assert.AreEqual(1, userBasket.BasketItems.Count);
        Assert.AreEqual(1, userBasket.BasketItems[0].StockItemId);
        Assert.AreEqual(2, userBasket.BasketItems[0].Count);
        Assert.AreEqual(220.31, userBasket.BasketItems[0].Cost);
    }

    [Test, Order(4)]
    public async Task DeleteItem_Success()
    {
        var result = await _controller.DeleteItem(1);

        Assert.That(result, Is.TypeOf<OkResult>());

        var userBasket = (await _controller.GetByUser(1) as OkObjectResult).Value as Basket;

        Assert.AreEqual(0, userBasket.BasketItems.Count);
    }

    [Test, Order(4)]
    public async Task DeleteItem_Failure()
    {
        Assert.That(() => _controller.DeleteItem(999), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find BasketItem with id: 999"));

        var userBasket = (await _controller.GetByUser(1) as OkObjectResult).Value as Basket;

        Assert.AreEqual(0, userBasket.BasketItems.Count);
    }

    [OneTimeTearDown]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }
}