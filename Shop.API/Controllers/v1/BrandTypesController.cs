using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.Contracts;
using Shop.API.Core;

namespace Shop.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BrandTypesController : Controller
    {
        private readonly IBrandTypeService _brandService;

        public BrandTypesController(IBrandTypeService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAll());
        }

        [HttpGet("get-by-brand/{brandId}")]
        public async Task<IActionResult> GetAllByBrand(long brandId)
        {
            return Ok(await _brandService.GetTypesByBrand(brandId));
        }

        [HttpGet("get-by-type/{typeId}")]
        public async Task<IActionResult> GetAllByType(long typeId)
        {
            return Ok(await _brandService.GetBrandsByType(typeId));
        }

        [Admin]
        [HttpPost("add")]
        public async Task<IActionResult> Add(BrandType brand)
        {
            return Ok(await _brandService.Add(brand));
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