using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Middleware;
using WebApplication.Services.Contracts;

namespace WebApplication.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Log> res =  await _logService.GetAll();

            return Ok(res);
        }

        [HttpGet("get-by-id")]
        public IActionResult GetById(long id)
        {
            return Ok(_logService.GetById(id));
        }

        [Admin]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            _logService.Delete(id);
            return Ok();
        }
    }
}