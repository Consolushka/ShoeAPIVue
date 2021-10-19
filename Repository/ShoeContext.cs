using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ShoeContext: DbContext
    {
        
        public ShoeContext(DbContextOptions<ShoeContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Shoe> Shoe { get; set; }
    }
}