using System.Collections.Generic;
using Core;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Middleware;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoeController: Controller
    {
        private readonly IShoeService _service;

        public ShoeController(IShoeService service)
        {
            _service = service;
        }
        
        [HttpGet("GetAll")]
        public List<Shoe> GetAll()
        {
            var res =  _service.GetAll();

            return res;
        }

        [HttpGet("GetById")]
        public Shoe GetById(long id)
        {
            return _service.GetById(id);
        }

        [Authorize]
        [HttpPost("Add")]
        public long Add(Shoe shoe)
        {
            return _service.Add(shoe);
        }

        [Authorize]
        [HttpPut("Update")]
        public Shoe Update(Shoe shoe)
        {
            return _service.Update(shoe);
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}