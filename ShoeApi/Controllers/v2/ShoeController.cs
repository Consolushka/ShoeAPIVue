using System.Collections.Generic;
using System.IO;
using Core.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Middleware;

namespace WebApplication.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]  
    public class ShoeController: Controller
    {
        private readonly IShoeService _service;
        private readonly IWebHostEnvironment _env;

        public ShoeController(IShoeService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }
        
        [MapToApiVersion("2.0")]
        [HttpGet("GetAll")]
        public List<Shoe> GetAll()
        {
            var res =  _service.GetAll();

            return res;
        }

        [MapToApiVersion("2.0")]
        [HttpGet("GetById")]
        public Shoe GetById(long id)
        {
            return _service.GetById(id);
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPost("Add")]
        public IActionResult Add(Shoe shoe)
        {
            var res = _service.Add(shoe);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPut("Update")]
        public IActionResult Update(Shoe shoe)
        {
            var res =  _service.Update(shoe);
            
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);
            return Ok();
        }
        
        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPost("SaveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httprequest = Request.Form;
                var requestFile = httprequest.Files[0];
                string fileName = requestFile.FileName;
                var PhysicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(PhysicalPath, FileMode.Create))
                {
                    requestFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch
            {
                return new JsonResult("error.png");
            }
        }
    }
}