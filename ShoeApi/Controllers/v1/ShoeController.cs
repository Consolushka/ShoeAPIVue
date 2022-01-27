using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        public ShoeController(IShoeService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }
        
        public ShoeController(IShoeService service)
        {
            _service = service;
        }
        
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Shoe> res =  await _service.GetAll();

            return Ok(res);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _service.GetById(id));
        }

        [Admin]
        [HttpPost("add")]
        public async Task<IActionResult> Add(ShoeVM shoeVm)
        {
            Shoe res = await _service.Add(shoeVm);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Admin]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(ShoeVM shoe, long id)
        {
            Shoe res = await _service.Update(shoe, id);
            
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [Admin]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.Delete(id);
            return Ok();
        }
        
        [Admin]
        [HttpPost("save-file")]
        private async Task<JsonResult> SaveFile()
        {
            try
            {
                var httprequest = Request.Form;
                var requestFile = httprequest.Files[0];
                string fileName = requestFile.FileName;
                var PhysicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(PhysicalPath, FileMode.Create))
                {
                    await requestFile.CopyToAsync(stream);
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