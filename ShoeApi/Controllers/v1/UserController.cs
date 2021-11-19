using System;
using System.Threading.Tasks;
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
            var response = await _userService.Register(userModel);
            
            if (response!=null)
            {
                MailSender.ConfirmRegistration(response);
                return Ok();
            }
            
            return BadRequest("User with this Email already exists");
        }

        [HttpPost("confirm-registration")]
        public IActionResult ConfirmRegistration(string key)
        {
            var Gkey = Guid.Parse(key);
            if (_userService.ConfirmUser(Gkey).Result)
            {
                return Ok();   
            }
            
            return BadRequest();
        }

        [Authorize]
        [HttpPost("get-by-id")]
        public UserResponse GetById(int id)
        {
            var u = new UserResponse(_userService.GetById(id));
            return u;
        }

        [Authorize]
        [HttpPost("update")]
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

        [Authorize]
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}