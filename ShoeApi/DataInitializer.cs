using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Middleware;
using Repository;

namespace WebApplication
{
    public static class DataInitializer
    {
        public static void SeedData(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ShoeContext>();

                SeedUsersIfNone(context);
                SeedBrandsIfNone(context);
                SeedShoesIfNone(context);

                context.SaveChanges();
            }
        }

        private static void SeedUsersIfNone(ShoeContext context)
        {
            if (!context.User.Any())
            {
                context.User.Add(new User()
                {
                    ConfirmString = new Guid(),
                    Email = "consolushka@gmail.com",
                    Password = Crypto.Encode("admin"),
                    IsActive = true,
                    UserName = "admin"
                });
            }
        }
        
        private static void SeedBrandsIfNone(ShoeContext context)
        {
            if (!context.Brand.Any())
            {
                context.Brand.AddRange(new List<Brand>()
                {
                    new Brand()
                    {
                        Name = "Nike"
                    },
                    new Brand()
                    {
                        Name = "Puma"
                    },
                });
            }
        }
        
        private static void SeedShoesIfNone(ShoeContext context)
        {
            if (!context.Shoe.Any())
            {
                context.Shoe.AddRange(new List<Shoe>()
                {
                    new Shoe()
                    {
                        Name = "Nike v.1",
                        BrandId = 1,
                        CreationTime = DateTime.Now,
                        PhotoFileName = "undefined.jpg"
                    },
                    new Shoe()
                    {
                        Name = "Puma v.1",
                        BrandId = 2,
                        CreationTime = DateTime.Now.AddDays(-1),
                        PhotoFileName = "undefined.jpg"
                    },
                });
            }
        }
    }
}