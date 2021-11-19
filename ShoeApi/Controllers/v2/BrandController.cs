using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Middleware;
using WebApplication.Services.Contracts;

namespace WebApplication.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [MapToApiVersion("2.0")]
        [HttpGet("get-all")]
        public List<Brand> GetAll()
        {
            var res =  _brandService.GetAll();

            return res;
        }

        [MapToApiVersion("2.0")]
        [HttpGet("get-by-id")]
        public Brand GetById(long id)
        {
            return _brandService.GetById(id);
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPost("add")]
        public IActionResult Add(Brand brand)
        {
            var res =_brandService.Add(brand);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPut("update")]
        public IActionResult Update(Brand brand)
        {
            var res =_brandService.Update(brand);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            _brandService.Delete(id);
            return Ok();
        }
    }
}