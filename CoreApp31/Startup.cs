using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp31.CustomFilters;
using CoreApp31.Models;
using CoreApp31.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreApp31
{
    public class Startup
    {
        /// <summary>
        /// The Ctor is injected with IConfiguration, it is resolved by the ConfigureAppConfiguration()
        /// The IConfiguration, interface reads appsettings.json file for all application level config
        /// e.g. ConnectionStrings, Loggining, any other custom configuiration values
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// IServiceCollection interface is resolved by Host class. This is a contract, is uses to 
        /// Register the application dependencies in Built-In DI Container of ASP.NET Core
        /// IServiceCollection uses ServiceDescriptor class to perform Lifecycle management of the Dependencies (aka service classes)
        /// Lifetime is managed by ServiceLifetime enum as SIngleton, Scoped and Transient
        /// Service class are, Security Classes, Controllers(), ControllersWithViews(), RazorViews(), CORS, Session,
        /// DbContext, Custom Business Service classes, etc.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // register the DbContext in DI Container
            services.AddDbContext<VodafoneDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("VodafoneAppConnection"));
            });


            // register the Custom Repository Services in DI Container
            services.AddScoped<IService<Category, int>, CategoryService>();
            services.AddScoped<IService<Product, int>, ProductService>();
            services.AddControllersWithViews(options => {
                // registering the custom filter in services
                // this will be executed when the exception occures in
                // any MVC controller
                options.Filters.Add(typeof(MyExceptionFilterAttribute));
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// This method is used to Manage the HTTP Request Pipeline using Middlewares. All middlewares are
        /// registered and executed using  IApplicationBuilder inside the Http Pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // developer exception
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(); // used to read all static files from wwwroot folder i.e. all js/css/image files

            app.UseRouting(); // Routing, loads and eveluate Route expressions for MVC and WEB API 

            app.UseAuthorization(); // User and Role Management

            // the HTTP Endpoint on ehich the ASP.NET Core app is available in dotnet processing
            app.UseEndpoints(endpoints =>
            {
                // execute a specifc controller by default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
