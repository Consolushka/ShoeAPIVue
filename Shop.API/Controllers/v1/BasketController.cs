using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Core;
using Shop.Data.Models;
using Shop.Services.Contracts;

namespace Shop.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BasketController: Controller
    {
        private readonly IBasketService _service;
        private readonly IBasketItemService _itemService;

        public BasketController(IBasketService service, IBasketItemService itemService)
        {
            _service = service;
            _itemService = itemService;
        }

        [HttpGet("get-by-user")]
        public async Task<IActionResult> GetByUser(long id)
        {
            return Ok(await _service.GetByUser(id));
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem(BasketItem basketItem)
        {
            await _itemService.Add(basketItem);
            return Ok();
        }

        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(long itemId, int newCount)
        {
            await _itemService.UpdateQuantity(itemId, newCount);
            return Ok();
        }

        [HttpDelete("delete-item")]
        public async Task<IActionResult> DeleteItem(long itemId)
        {
            await _itemService.Delete(itemId);
            return Ok();
        }
        
    }
}