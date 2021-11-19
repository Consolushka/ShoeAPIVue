using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;
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
        public IActionResult GetAll()
        {
            var res =  _brandService.GetAll();

            return Ok(res);
        }

        [MapToApiVersion("2.0")]
        [HttpGet("get-by-id")]
        public IActionResult GetById(long id)
        {
            return Ok(_brandService.GetById(id));
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPost("add")]
        public IActionResult Add(BrandVM brand)
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
        [HttpPut("update/{id}")]
        public IActionResult Update(BrandVM brandVm, long id)
        {
            var res =_brandService.Update(brandVm, id);
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