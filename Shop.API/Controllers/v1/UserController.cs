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
            UserResponse response = await _userService.Authenticate(user);
            //TODO: FIX
            if (response == null)
                return BadRequest("Username or password is incorrect");

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserVM userVm)
        {
            User response = await _userService.Register(userVm);
            
            if (response!=null)
            {
                MailSender.ConfirmRegistration(response);
                return Ok();
            }
            
            return BadRequest("User with this Email already exists");
        }

        [HttpPost("confirm-registration")]
        public async Task<IActionResult> ConfirmRegistration(string key)
        {
            var Gkey = Guid.Parse(key);
            if (await _userService.ConfirmUser(Gkey))
            {
                return Ok();   
            }
            
            return BadRequest();
        }

        [Authorize]
        [HttpGet("get-by-id")]
        public IActionResult GetById(int id)
        {
            UserResponse u = new UserResponse(_userService.GetById(id));
            return Ok(u);
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(UserVM userVm, long id)
        {
            User u =await _userService.Update(userVm, id);
            if (u == null)
            {
                return BadRequest("Cannot find your Account");
            }
            
            MailSender.ConfirmUpdate(u);
            return Ok();
        }

        [Authorize]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<User> users = await _userService.GetAll();
            return Ok(users);
        }
    }
}