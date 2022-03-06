﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
    public class GoodsController: Controller
    {
        private readonly IGoodsService _service;
        private readonly IWebHostEnvironment _env;

        public GoodsController(IGoodsService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }
        
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Good> res =  await _service.GetAll();

            return Ok(res);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _service.GetById(id));
        }

        [Admin]
        [HttpPost("add")]
        public async Task<IActionResult> Add(GoodVm goodVm)
        {
            Good res = await _service.Add(goodVm);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Admin]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(GoodVm good, long id)
        {
            Good res = await _service.Update(good, id);
            
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