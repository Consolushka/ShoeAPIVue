using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.ViewModels;
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
        public IActionResult GetAll()
        {
            var res =  _brandService.GetAll();

            return Ok(res);
        }

        [HttpGet("get-by-id")]
        public IActionResult GetById(long id)
        {
            return Ok(_brandService.GetById(id));
        }

        [Admin]
        [HttpPost("add")]
        public async Task<IActionResult> Add(BrandVM brand)
        {
            var res = await _brandService.Add(brand);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Admin]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(BrandVM brandVm, long id)
        {
            var res =await _brandService.Update(brandVm, id);
            if (res == null)
            {
                return BadRequest();
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