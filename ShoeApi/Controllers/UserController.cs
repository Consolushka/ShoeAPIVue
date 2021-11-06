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
        public async Task<IActionResult> Register(string email, string password)
        {
            var encPass = Crypto.Encode(password);
            UserModel userModel = new UserModel(email, encPass);
            var response = await _userService.Register(userModel);
            
            if (response)
            {
                MailSender.ConfirmRegistration(userModel);
                return Ok();
            }
            
            return BadRequest("User with this Email already exists");
        }

        [HttpPost("ConfirmRegistration")]
        public IActionResult ConfirmRegistration(Guid key)
        {
            if (_userService.ConfirmRegistration(key).Result)
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

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}