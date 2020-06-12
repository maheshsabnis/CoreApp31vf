using System;
using CoreApp31.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CoreApp31.Areas.Identity.IdentityHostingStartup))]
namespace CoreApp31.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<VodafoneWebAuthDbContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("VodafoneWebAuthDbContextConnection")));

            //    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<VodafoneWebAuthDbContext>();
            //});
        }
    }
}