using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data.Models;
using Shop.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.Contracts;
using Shop.API.Core;

namespace Shop.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]  
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserVM user)
        {
            try
            {
                return Ok(await _userService.Authenticate(user));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserVM userVm)
        {
            try
            {
                User response = await _userService.Register(userVm);
                MailSender.ConfirmRegistration(response);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("confirm-registration")]
        public async Task<IActionResult> ConfirmRegistration(string key)
        {
            try
            {
                var Gkey = Guid.Parse(key);
                await _userService.ConfirmUser(Gkey);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet("get-by-id")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(new UserResponse(_userService.GetById(id)));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(UserVM userVm, long id)
        {
            try
            {
                User u =await _userService.Update(userVm, id);
            
                MailSender.ConfirmUpdate(u);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }
    }
}