using System;
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
            return Ok(await _userService.Authenticate(user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserVM userVm)
        {
            User response = await _userService.Register(userVm);
            MailSender.ConfirmRegistration(response);
            return Ok();
        }
        
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmRegistration(Guid key)
        {
            await _userService.ConfirmUser(key);
            return Ok();
        }

        [Authorize]
        [HttpGet("get-by-id")]
        public IActionResult GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(UserVM userVm, long id)
        {
            User u = await _userService.Update(userVm, id);

            MailSender.ConfirmUpdate(u);
            return Ok();
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(string password, long id)
        {
            await _userService.ChangePassword(password, id);
            MailSender.ConfirmUpdate(_userService.GetById(id));
            return Ok();
        }

        [Authorize]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }
    }
}