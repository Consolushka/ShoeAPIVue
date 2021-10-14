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
    }
}
