using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.ViewModels;
using WebApplication.Middleware;
using WebApplication.Services.Contracts;

namespace WebApplication.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]  
    public class GoodsController: Controller
    {
        private readonly IGoodsService _service;
        private readonly IWebHostEnvironment _env;

        public GoodsController(IGoodsService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }
        
        [MapToApiVersion("2.0")]
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var res =  _service.GetAll();

            return Ok(res);
        }

        [MapToApiVersion("2.0")]
        [HttpGet("get-by-id")]
        public IActionResult GetById(long id)
        {
            return Ok(_service.GetById(id));
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPost("add")]
        public IActionResult Add(GoodVm goodVm)
        {
            var res = _service.Add(goodVm);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPut("update/{id}")]
        public IActionResult Update(GoodVm goodVm, long id)
        {
            var res =  _service.Update(goodVm, id);
            
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);
            return Ok();
        }
        
        [Admin]
        [MapToApiVersion("2.0")]
        [HttpPost("save-file")]
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