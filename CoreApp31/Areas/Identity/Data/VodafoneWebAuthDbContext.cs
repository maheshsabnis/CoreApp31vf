using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreApp31.Data
{
    /// <summary>
    /// IdentityDbContext class will manage DbSet for all Identity Tables
    /// e.g. AspNetUsers, AspNetRoles, etc....
    /// </summary>
    public class VodafoneWebAuthDbContext : IdentityDbContext<IdentityUser>
    {
        public VodafoneWebAuthDbContext(DbContextOptions<VodafoneWebAuthDbContext> options)
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
    }
}
