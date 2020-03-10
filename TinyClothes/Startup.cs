using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinyClothes.Data;

namespace TinyClothes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigDbConext(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("ClothesDB"));
        }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddControllersWithViews();

            services.AddDbContext<StoreContext>(ConfigDbConext);
            //services.AddDbContext<StoreContext>
            //(
            //    options => options.UseSqlServer("con string goes here")
            //);
            string connection = Configuration.GetConnectionString("ClothesDB");
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Same as above.
            services.AddHttpContextAccessor();
#if DEBUG
            // Only add during DEBUG mode
            // builder.AddRazorRuntimeCompilation();
#endif
            // Add & configure session.
            services.AddDistributedMemoryCache();

            services.AddSession
            (
                options => 
                {
                    options.Cookie.Name = ".TinyClothes.Session";
                    options.IdleTimeout = TimeSpan.FromMinutes(20);
                    // Session cookie always gets created even
                    // if user does not accept cookie policy
                    options.Cookie.IsEssential = true;
                }
            );
        }

        // This method gets called by the runtime.
        //Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days.
                // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Allows session data to be accessed.
            app.UseSession();

            app.UseStatusCodePagesWithRedirects("/Home/ErrorCustom?code={0}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
