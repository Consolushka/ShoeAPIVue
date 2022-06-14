﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shop.Services.Contracts;
using Shop.API.Core;

namespace Shop.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAll());
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _brandService.GetById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [HttpPost("add")]
        public async Task<IActionResult> Add(BrandVM brand)
        {
            if (brand == null)
            {
                return BadRequest("Null entity");
            }
            try
            {
                return Ok(await _brandService.Add(brand));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Admin]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(BrandVM brandVm, long id)
        {
            if (brandVm==null)
            {
                return BadRequest("Null entity");
            }
            try
            {
                await _brandService.Update(brandVm, id);
                return Ok();
            }
            catch(Exception ex)
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
                await _brandService.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}