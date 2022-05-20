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
            return Ok(await _logService.GetAll());
        }

        [HttpGet("get-by-id")]
        public IActionResult GetById(long id)
        {
            try
            {
                return Ok(_logService.GetById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _logService.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}