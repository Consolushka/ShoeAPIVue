﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.Contracts;
using Shop.API.Core;

namespace Shop.API.Controllers.V1
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
            return Ok(await _service.GetAll());
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _service.GetById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [HttpPost("add")]
        public async Task<IActionResult> Add(GoodVm goodVm)
        {
            try
            {
                return Ok(await _service.Add(goodVm));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(GoodVm good, long id)
        {
            try
            {
                await _service.Update(good, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
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