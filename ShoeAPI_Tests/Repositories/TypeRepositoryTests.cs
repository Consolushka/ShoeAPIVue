using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebApplication.Data;
using WebApplication.Middleware;
using WebApplication.Repository.EntityRepository;
using WebApplication.Services.ModelServices;
using Type = WebApplication.Data.Models.Type;

namespace ShoeAPI_Tests.Repositories
{
    public class TypeRepositoryTests
    {
        
        private static DbContextOptions<ShopContext> _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "ShoeTest")
            .Options;

        private ShopContext _context;
        private TypeRepository _repository;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new ShopContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            _repository = new TypeRepository(_context);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public async Task AddType()
        {
            Type type = new Type()
            {
                Name = "Shoe"
            };
            var res = await _repository.Add(type);
            Assert.AreEqual(res.Id, 1);
            Assert.AreEqual(res.Name, "Shoe");
        }

        [Test, Order(2)]
        public void AddExistingType()
        {
            Type type = new Type()
            {
                Name = "Shoe"
            };
            Assert.That(async ()=> await _repository.Add(type), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Type with name Shoe is already exists"));
        }

        [Test, Order(3)]
        public async Task GetAll()
        {
            List<Type> res = await _repository.GetAll();
            Assert.AreEqual(res.Count, 1);
        }

        [Test, Order(4)]
        public async Task GetById()
        {
            var res = await _repository.GetById(1);
            Assert.AreEqual(res.Id, 1);
            Assert.AreEqual(res.Name, "Shoe");
        }
        
        [Test, Order(5)]
        public void GetById_Err()
        {
            Assert.That(async ()=>await _repository.GetById(2), Throws.Exception.TypeOf<Exception>().With.Message.EqualTo("Cannot find Type with id: 2"));
        }

        // [Test, Order(6)]
        // public async Task Update()
        // {
        //     Type type = new Type()
        //     {
        //         Id = 1,
        //         Name = "Shoes"
        //     };
        //     var res = await _repository.Update(type);
        //     Assert.AreEqual(res.Id, 1);
        //     Assert.AreEqual(res.Name, "Shoes");
        //}

        [Test, Order(7)]
        public async Task Delete()
        {
            await _repository.Delete(1);
        }
    }
}