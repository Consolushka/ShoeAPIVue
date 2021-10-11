using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ShoeAPIVue.Data;
using ShoeAPIVue.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace ShoeAPIVue.Controllers
{
    public class AccountController : Controller
    {
        private ShoeContext _context;

        public AccountController(ShoeContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.User.FirstOrDefaultAsync(u =>
                    u.Email == login.Email && u.Password == login.Password);
                if (user != null)
                {
                    await Authenticate(login.Email);

                    return Ok();
                }
                ModelState.AddModelError("", "Incorrect Password Or Email");
                return NotFound();
            }
            return StatusCode(400);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Account/Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.User.FirstOrDefaultAsync(u =>
                    u.Email == model.Email);
                if (user == null)
                {
                    await _context.User.AddAsync(new User(model));
                    await Authenticate(model.Email);

                    return Ok();
                }
                ModelState.AddModelError("", "User with this Email already exists");
                return NotFound();
            }
            return StatusCode(400);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        
        // GET
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return StatusCode(200);
        }
    }
}