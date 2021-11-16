using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Data.ViewModels;
using WebApplication.Middleware;
using WebApplication.Services.Contracts;

namespace WebApplication.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]  
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [MapToApiVersion("2.0")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            var response = await _userService.Register(userModel);
            
            if (response!=null)
            {
                MailSender.ConfirmRegistration(response);
                return Ok();
            }
            
            return BadRequest("User with this Email already exists");
        }

        [MapToApiVersion("2.0")]
        [HttpPost("ConfirmRegistration")]
        public IActionResult ConfirmRegistration(string key)
        {
            var Gkey = Guid.Parse(key);
            if (_userService.ConfirmUser(Gkey).Result)
            {
                return Ok();   
            }
            
            return BadRequest();
        }

        [MapToApiVersion("2.0")]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest("Username or password is incorrect");

            return Ok(response);
        }

        [Authorize]
        [MapToApiVersion("2.0")]
        [HttpPost("GetById")]
        public UserResponse GetById(int id)
        {
            var u = new UserResponse(_userService.GetById(id));
            return u;
        }

        [Authorize]
        [MapToApiVersion("2.0")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(User user)
        {
            var u =await _userService.Update(user);
            if (u == null)
            {
                return BadRequest("Cannot find your Account");
            }
            
            MailSender.ConfirmUpdate(u);
            return Ok();
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}