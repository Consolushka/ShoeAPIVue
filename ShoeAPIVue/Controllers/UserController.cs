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

namespace ShoeAPIVue.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private ShoeContext _context;
        private readonly IWebHostEnvironment _env;

        public UserController(ShoeContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        
        
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.User.FirstOrDefaultAsync(u =>
                    u.Email == login.Email && u.Password == login.Password);
                if (user != null)
                {
                    Authenticate(login.Email);
                    return Ok("Fine");
                }
                return BadRequest("Incorrect User email or password");
            }
            return BadRequest("Invalid Data");
        }

        // POST: api/User
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.User.FirstOrDefaultAsync(u =>
                    u.Email == model.Email);
                if (user == null)
                {
                    await _context.User.AddAsync(new User(model));
                    _context.SaveChanges();
                
                    return Ok();
                }
                return BadRequest("User with this Email already exists");
            }
            return BadRequest("Invalid Data");
        }

        [HttpGet]
        [Route("IsAuth")]
        public bool IsAuth()
        {
            string n = User.Identity.Name;
            if (n != null)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser()
        {
            try
            {
                User u = _context.User.First(u => u.Email == User.Identity.Name);
                return Ok(u);
            }
            catch
            {
                User u = new User();
                return BadRequest("canot find");
            }
        }
        

        private async Task<string> Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            return User.Identity.Name;
        }
        
        [HttpPost]
        [Route("Logout")]
        // GET
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return StatusCode(200);
        }
    }
}
