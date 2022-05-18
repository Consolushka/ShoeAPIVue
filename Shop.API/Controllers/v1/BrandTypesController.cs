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
            IEnumerable<BrandType> res =  await _brandService.GetAll();

            return Ok(res);
        }

        [HttpGet("get-by-brand/{brandId}")]
        public async Task<IActionResult> GetAllByBrand(long brandId)
        {
            IEnumerable<Type> res =  await _brandService.GetTypesByBrand(brandId);

            return Ok(res);
        }

        [HttpGet("get-by-type/{typeId}")]
        public async Task<IActionResult> GetAllByType(long typeId)
        {
            IEnumerable<Brand> res =  await _brandService.GetBrandsByType(typeId);

            return Ok(res);
        }

        [Admin]
        [HttpPost("add")]
        public async Task<IActionResult> Add(BrandType brand)
        {
            BrandType res = await _brandService.Add(brand);
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