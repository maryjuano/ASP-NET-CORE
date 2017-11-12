using Microsoft.AspNetCore.Identity;
using Stark.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stark.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StarkDbContext _starkDbContext;
        private readonly UserManager<StarkIdentityUser> _userManager;
        public DbInitializer(UserManager<StarkIdentityUser> userManager, StarkDbContext starkDbContext)
        {
            _userManager = userManager;
            _starkDbContext = starkDbContext;
        }

        //This example just creates an Administrator role and one Admin users
        public async void Initialize()
        {
            //create database schema if none exists
            _starkDbContext.Database.EnsureCreated();
           
            //Create the default Admin account and apply the Administrator role     
            var admin = new StarkIdentityUser
            {
                UserName = "admin", FirstName = "Tony", LastName = "Stark", EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(admin, "ironman143"); 
            

        }
    }
}
