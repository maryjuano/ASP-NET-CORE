﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stark.DAL.Models;

namespace Stark.DAL
{
    public class StarkIdentityDbContext : IdentityDbContext<StarkIdentityUser, StarkIdentityRole, string> 
    {
        public StarkIdentityDbContext(DbContextOptions<StarkIdentityDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }      

        public DbSet<StarkIdentityUser> StarkIdentityUser { get; set; }
    } 
}
