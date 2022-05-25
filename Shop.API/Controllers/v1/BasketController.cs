using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Core;
using Shop.Services.Contracts;

namespace Shop.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BasketController: Controller
    {
        private readonly IBasketService _service;

        public BasketController(IBasketService service)
        {
            _service = service;
        }

        [HttpGet("get-by-user")]
        public async Task<IActionResult> GetByUser(long id)
        {
            return Ok(await _service.GetByUser(id));
        }
    }
}