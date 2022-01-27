using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
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
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Brand> res =  await _brandService.GetAll();

            return Ok(res);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _brandService.GetById(id));
        }

        [Admin]
        [HttpPost("add")]
        public async Task<IActionResult> Add(BrandVM brand)
        {
            Brand res = await _brandService.Add(brand);
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
            Brand res =await _brandService.Update(brandVm, id);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Admin]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _brandService.Delete(id);
            return Ok();
        }
    }
}