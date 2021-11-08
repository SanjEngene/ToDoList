using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using ToDoproj.Repository;
using ToDoproj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace ToDoproj
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }
        public Startup()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();     
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    opts.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("Users", policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimsIdentity.DefaultRoleClaimType);
                });
                opts.AddPolicy("Admin", policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, "admin");
                });
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IToDoRepository, ToDoRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}