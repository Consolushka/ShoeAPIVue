using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.ViewModels;
using WebApplication.Middleware;
using WebApplication.Services.Contracts;

namespace WebApplication.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogService _brandService;

        public LogController(ILogService brandService)
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
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            _brandService.Delete(id);
            return Ok();
        }
    }
}