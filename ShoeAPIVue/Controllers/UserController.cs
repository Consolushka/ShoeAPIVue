using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeAPIVue.Data;
using ShoeAPIVue.Models;

namespace ShoeAPIVue.Controllers
{
    [Route("api/[controller]")]
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
        
        // GET: api/User
        [HttpGet]
        public async Task<IEnumerable<Shoe>> Get()
        {
            return await _context.Shoe.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [Route("Register")]
        [HttpPost]
        public IActionResult Register()
        {
            return Ok();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
