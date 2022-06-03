using System;
using System.Threading.Tasks;
using Shop.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.Contracts;
using Shop.API.Core;

namespace Shop.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TypesController : Controller
    {
        private readonly ITypeService _service;
        
        public TypesController(ITypeService service)
        {
            _service = service;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [MapToApiVersion("1.0")]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _service.GetById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [MapToApiVersion("1.0")]
        [HttpPost("add")]
        public async Task<IActionResult> Add(TypeVM type)
        {
            try
            {
                return Ok(await _service.Add(type));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [MapToApiVersion("1.0")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(TypeVM typeVm, long id)
        {
            try
            {
                await _service.Update(typeVm, id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [MapToApiVersion("1.0")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}