using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Middleware;
using WebApplication.Services.Contracts;

namespace WebApplication.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("get-all")]
        public List<Brand> GetAll()
        {
            var res =  _brandService.GetAll();

            return res;
        }

        [HttpGet("get-by-id")]
        public IActionResult GetById(long id)
        {
            return Ok(_brandService.GetById(id));
        }

        [Admin]
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
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            _brandService.Delete(id);
            return Ok();
        }
    }
}