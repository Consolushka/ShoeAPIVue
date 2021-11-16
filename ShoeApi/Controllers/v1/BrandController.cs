using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Exceptions;
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

        [HttpGet("GetAll")]
        public List<Brand> GetAll()
        {
            var res =  _brandService.GetAll();

            return res;
        }

        [HttpGet("GetById")]
        public IActionResult GetById(long id)
        {
            try
            {
                var brand = _brandService.GetById(id);
                return Ok(brand);
            }
            catch (NullEntityException ex)
            {
                return BadRequest($"{ex.Message} {ex.Id}");
            }
        }

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

        [Admin]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            _brandService.Delete(id);
            return Ok();
        }
    }
}