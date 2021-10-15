using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeAPIVue.Data;
using ShoeAPIVue.Models;
using ShoeAPIVue.Entities;
using ShoeAPIVue.Services;

namespace ShoeAPIVue.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private ShoeContext _context;
        private readonly IWebHostEnvironment _env;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
            {
                return BadRequest("Cannot find user with this email and password");
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel model)
        {
            var response = await _userService.Register(model);

            if (response == null)
            {
                return BadRequest("Error");
            }

            return Ok(response);
        }
    }
}
