using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;
using WebApplication.Middleware;
using WebApplication.Services.Contracts;

namespace WebApplication.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]  
    public class ShoeController: Controller
    {
        private readonly IShoeService _service;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ShoeController(IShoeService service, IWebHostEnvironment env, IMapper mapper)
        {
            _service = service;
            _env = env;
            _mapper = mapper;
        }
        
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var res =  _service.GetAll();

            return Ok(res);
        }

        [HttpGet("get-by-id")]
        public IActionResult GetById(long id)
        {
            return Ok(_service.GetById(id));
        }

        [Admin]
        [HttpPost("add")]
        public IActionResult Add(ShoeVM shoeVm)
        {
            var res = _service.Add(shoeVm);
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [HttpPut("update/{id}")]
        public IActionResult Update(ShoeVM shoe, long id)
        {
            var res =  _service.Update(shoe, id);
            
            if (res == null)
            {
                return BadRequest("Server Error");
            }
            return Ok();
        }

        [Admin]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);
            return Ok();
        }
        
        [Admin]
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