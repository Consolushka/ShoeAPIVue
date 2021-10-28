using System.Collections.Generic;
using System.IO;
using Core.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Middleware;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoeController: Controller
    {
        private readonly IShoeService _service;
        private readonly IWebHostEnvironment _env;

        public ShoeController(IShoeService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
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
        
        [Authorize]
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