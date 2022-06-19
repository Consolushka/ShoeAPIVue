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

namespace Shop.API.Tests.Controllers;

public class BrandTypesControllerTests
{
    private ShopContext _context;

    private IBrandTypeService _brandService;
    private BrandTypesController _controller;

    [OneTimeSetUp]
    public void Setup()
    {
        _context = DBSeeding.CreateDb();

        var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new Mapper()); });
        var mapper = mapperConfig.CreateMapper();

        _brandService = new BrandTypeService(new BrandTypeRepository(_context));

        _controller = new BrandTypesController(_brandService);
    }

    [Test]
    [Order(1)]
    public async Task HttpGet_GetAll()
    {
        var actionResult = await _controller.GetAll();
        var actionResultData = (actionResult as OkObjectResult).Value as List<BrandType>;

        Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
        Assert.AreEqual(3, actionResultData.Count);

        Assert.AreEqual("Nike", actionResultData.First(bt => bt.Id == 1).Brand.Name);
        Assert.AreEqual("Shoe", actionResultData.First(bt => bt.Id == 1).Type.Name);

        Assert.AreEqual("Nike", actionResultData.First(bt => bt.Id == 2).Brand.Name);
        Assert.AreEqual("T-Shirt", actionResultData.First(bt => bt.Id == 2).Type.Name);

        Assert.AreEqual("Puma", actionResultData.First(bt => bt.Id == 3).Brand.Name);
        Assert.AreEqual("Shoe", actionResultData.First(bt => bt.Id == 3).Type.Name);
    }

    [Test]
    [Order(2)]
    public async Task HttpGet_GetAllByBrand()
    {
        var actionResult = await _controller.GetAllByBrand(1);

        Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

        var actionResultData = (actionResult as OkObjectResult).Value as List<Type>;
        var res = actionResultData.OrderBy(bt => bt.Id).ToList();

        Assert.AreEqual(2, res.Count);

        Assert.AreEqual(1, res[0].Id);
        Assert.AreEqual(2, res[1].Id);
    }

    [Test]
    [Order(3)]
    public void HttpGet_GetAllByBrand_NoBrand()
    {
        Assert.That(()=>_controller.GetAllByBrand(999), Throws.Exception.TypeOf<System.NullReferenceException>().With.Message.EqualTo("Cannot find this Brand"));
    }

    [Test]
    [Order(4)]
    public async Task HttpGet_GetAllByType()
    {
        var actionResult = await _controller.GetAllByType(1);

        Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

        var actionResultData = (actionResult as OkObjectResult).Value as List<Brand>;
        var res = actionResultData.OrderBy(bt => bt.Id).ToList();

        Assert.AreEqual(2, res.Count);

        Assert.AreEqual(1, res[0].Id);
        Assert.AreEqual(2, res[1].Id);
    }

    [Test]
    [Order(5)]
    public void HttpGet_GetAllByType_NoType()
    {
        Assert.That(() => _controller.GetAllByType(999), Throws.Exception.TypeOf<System.NullReferenceException>().With.Message.EqualTo("Cannot find this Type"));
    }

    [Test]
    [Order(6)]
    public async Task HttpPost_AddBrandType_Success()
    {
        var allBeforeAdding = await _controller.GetAll();
        var actionResult = await _controller.Add(new BrandType()
        {
            TypeId = 2,
            BrandId = 2
        });
        var allAfterAdding = await _controller.GetAll();
        var allAfterRes = ((allAfterAdding as OkObjectResult).Value as List<BrandType>).OrderBy(bt => bt.Id).ToList();
        var allBeforeRes = ((allBeforeAdding as OkObjectResult).Value as List<BrandType>).OrderBy(bt => bt.Id).ToList();

        Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
        Assert.AreEqual(allBeforeRes.Count + 1, allAfterRes.Count);
        Assert.AreEqual(2, allAfterRes[3].BrandId);
        Assert.AreEqual(2, allAfterRes[3].TypeId);
    }

    [Test]
    [Order(7)]
    public async Task HttpPost_AddBrandType_Err_AlreadyExists()
    {
        Assert.That(() => _controller.Add(new BrandType() { TypeId = 1, BrandId = 1 }), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Same BrandType already exists"));
        var allBeforeAdding = await _controller.GetAll();

        var allAfterAdding = await _controller.GetAll();
        var allAfterRes = (allAfterAdding as OkObjectResult).Value as List<BrandType>;
        var allBeforeRes = (allBeforeAdding as OkObjectResult).Value as List<BrandType>;

        Assert.AreEqual(allBeforeRes.Count, allAfterRes.Count);
    }

    [OneTimeTearDown]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }
}