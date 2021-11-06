using System.Collections.Generic;
using Core.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Middleware;

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

        [Authorize]
        [Admin]
        [HttpPost("Add")]
        public IActionResult Add(Brand brand)
        {
            var res =_brandService.Add(brand);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Authorize]
        [Admin]
        [HttpPut("Update")]
        public IActionResult Update(Brand brand)
        {
            var res =_brandService.Update(brand);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Authorize]
        [Admin]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            _brandService.Delete(id);
            return Ok();
        }
    }
}