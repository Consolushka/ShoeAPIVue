using System.Collections.Generic;
using Core;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("GetAll")]
        public List<Brand> GetAll()
        {
            var res =  _brandService.GetAll();

            return res;
        }

        [HttpGet("GetById")]
        public Brand GetById(long id)
        {
            return _brandService.GetById(id);
        }

        [HttpPost("Add")]
        public long Add(Brand brand)
        {
            return _brandService.Add(brand);
        }

        [HttpPut("Update")]
        public Brand Update(Brand brand)
        {
            return _brandService.Update(brand);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            _brandService.Delete(id);
            return Ok();
        }
    }
}