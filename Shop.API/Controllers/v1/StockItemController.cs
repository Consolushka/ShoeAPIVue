using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Models;
using Shop.Services.Contracts;

namespace Shop.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StockItemController: Controller
    {
        private readonly IStockItemService _service;

        public StockItemController(IStockItemService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(StockItem stockItem)
        {
            var res = await _service.Add(stockItem);
            return Ok(res);
        }

        [HttpPut("write-off")]
        public async Task<IActionResult> WriteOff(long id, int minusCount)
        {
            await _service.WriteOff(id, minusCount);
            return Ok();
        }

        [HttpPut("refill")]
        public async Task<IActionResult> Refill(long id, int plusCount)
        {
            await _service.Refill(id, plusCount);
            return Ok();
        }
        
    }
}