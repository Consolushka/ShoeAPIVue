using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.ViewModels;
using WebApplication.Middleware;
using WebApplication.Services.Contracts;

namespace WebApplication.Controllers.V1
{
    [ApiController]
    [ApiVersion("2.0", Deprecated = false)]
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
            var res =  await _service.GetAll();

            return Ok(res);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _service.GetById(id));
        }

        [Admin]
        [MapToApiVersion("1.0")]
        [HttpPost("add")]
        public async Task<IActionResult> Add(TypeVM type)
        {
            var res =await _service.Add(type);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [MapToApiVersion("1.0")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(TypeVM typeVm, long id)
        {
            var res =await _service.Update(typeVm, id);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [MapToApiVersion("1.0")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}