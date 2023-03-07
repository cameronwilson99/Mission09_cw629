using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;

namespace Bookstore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //tells asp.net to use MVC pattern
            services.AddControllersWithViews();

            //adds context file
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();

            //enables use of razor pages
            services.AddRazorPages();

            //allows the use of sessions (keep cart full for user during session)
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // corresponds to the wwwroot folder
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                //passes both the category and the page number to the url to be routed, dynamically changing the data visualization
                endpoints.MapControllerRoute("CategoryPage", "{bookCategory}/Page{pageNum}", new { controller = "Home", action = "Index" });

                //executes endpoints, changes the url depending on the page selected.
                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum=1});

                //used for when only a category is given, sets the page number to the first page so we aren't stuck on another page when we change categories
                endpoints.MapControllerRoute("Category", "{bookCategory}", new { controller="Home", action = "Index", pageNum=1});

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages(); //allows razor pages to be displayed
            });
        }
    }
}
