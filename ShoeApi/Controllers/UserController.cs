using System;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Core;
using Core.Contracts;
using Entities.Models;
using Entities.Support;
using Microsoft.AspNetCore.Mvc;
using Middleware;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest("Username or password is incorrect");

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            userModel.Password = Crypto.Encode(userModel.Password);
            userModel.RoleId = 1;
            userModel.IsConfirmed = false;
            var response = await _userService.Register(userModel);
            
            if (response!=null)
            {
                MailSender.ConfirmRegistration(response);
                return Ok();
            }
            
            return BadRequest("User with this Email already exists");
        }

        [HttpPost("ConfirmRegistration")]
        public IActionResult ConfirmRegistration(string key)
        {
            var Gkey = Guid.Parse(key);
            if (_userService.ConfirmRegistration(Gkey).Result)
            {
                return Ok();   
            }
            
            return BadRequest();
        }

        [Authorize]
        [HttpPost("GetById")]
        public UserResponse GetById(int id)
        {
            var u = new UserResponse(_userService.GetById(id));
            return u;
        }

        public async Task<IActionResult> Update(UserModel userModel)
        {
            var u =await _userService.Update(userModel);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}