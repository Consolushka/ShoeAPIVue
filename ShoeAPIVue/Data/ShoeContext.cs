using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoeAPIVue.Models;

namespace ShoeAPIVue.Data
{
    public class ShoeContext : DbContext
    {
        public ShoeContext(DbContextOptions<ShoeContext> options)
            : base(options)
        {
        }

        public DbSet<Shoe> Shoe { get; set; }
        public DbSet<Brand> Brand { get; set; }
    }
}
