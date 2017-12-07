using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stark.DAL
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StarkIdentityDbContext _starkDbContext;
        private readonly UserManager<StarkIdentityUser> _userManager;
        public DbInitializer(UserManager<StarkIdentityUser> userManager, StarkIdentityDbContext starkDbContext)
        {
            _userManager = userManager;
            _starkDbContext = starkDbContext;
        }

        //This example just creates an Administrator role and one Admin users
        public async void Initialize()
        {
            //create database schema if none exists
            _starkDbContext.Database.EnsureCreated();

            List<StarkIdentityUser> users = new List<StarkIdentityUser>();
            
            var admin = new StarkIdentityUser
            {
                UserName = "admin", FirstName = "Tony", LastName = "Stark", EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("P")
            };
            var password = new PasswordHasher<StarkIdentityUser>();
            var hashed = password.HashPassword(admin, "ironman143");
            admin.PasswordHash = hashed;

            users.Add(admin);

            var rex = new StarkIdentityUser
            {
                UserName = "maryjuano",
                FirstName = "Rex Joseph",
                MiddleName = "Fabillar",
                LastName = "Cadiao",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("B")
            };
            
            var hashed2 = password.HashPassword(rex, "typewriter");
            rex.PasswordHash = hashed2;

            users.Add(rex);

            var aly = new StarkIdentityUser
            {
                UserName = "alydmngz",
                FirstName = "Alyssa Janelle",
                MiddleName = "Dominguez",
                LastName = "Cadiao",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("N")
            };

            var hashed3 = password.HashPassword(aly, "iloverexsobadss");
            aly.PasswordHash = hashed3;

            users.Add(aly);

            foreach (var user in users)
            {
                if (!_starkDbContext.Users.Any(u => u.UserName == user.UserName))
                {
                    _starkDbContext.Users.Add(user);
                }               
            }

           

            await _starkDbContext.SaveChangesAsync();
        }
    }
}
