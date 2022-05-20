using System;
using System.Threading.Tasks;
using Shop.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.Contracts;
using Shop.API.Core;

namespace Shop.API.Controllers.V2
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
        public async Task<IActionResult> Register(UserVM userVm)
        {
            var response = await _userService.Register(userVm);
            
            if (response!=null)
            {
                MailSender.ConfirmRegistration(response);
                return Ok();
            }
            
            return BadRequest("User with this Email already exists");
        }

        [MapToApiVersion("2.0")]
        [HttpPost("confirm-registration")]
        public IActionResult ConfirmRegistration(string key)
        {
            var Gkey = Guid.Parse(key);
            _userService.ConfirmUser(Gkey);
            
            return BadRequest();
        }

        [MapToApiVersion("2.0")]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserVM vm)
        {
            var response = _userService.Authenticate(vm);

            if (response == null)
                return BadRequest("Username or password is incorrect");

            return Ok(response);
        }

        [Authorize]
        [MapToApiVersion("2.0")]
        [HttpGet("get-by-id")]
        public UserResponse GetById(int id)
        {
            var u = new UserResponse(_userService.GetById(id));
            return u;
        }

        [Authorize]
        [MapToApiVersion("2.0")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(UserVM userVm, long id)
        {
            var u =await _userService.Update(userVm, id);
            if (u == null)
            {
                return BadRequest("Cannot find your Account");
            }
            
            MailSender.ConfirmUpdate(u);
            return Ok();
        }

        [Admin]
        [MapToApiVersion("2.0")]
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}