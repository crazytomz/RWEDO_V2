using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RWEDO.DataTransferObject;
using RWEDO.Services;
using StructureMap;

namespace RWEDO
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<RWEDODbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("DBConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<RWEDODbContext>()
            .AddDefaultTokenProviders();
            services.AddMvc();
            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    _config["EmailSender:Host"],
                    _config.GetValue<int>("EmailSender:Port"),
                    _config.GetValue<bool>("EmailSender:EnableSSL"),
                    _config["EmailSender:UserName"],
                    _config["EmailSender:Password"]
                )
            );
            // Configure your policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdminPolicy",
                  policy => policy.RequireUserName("sadmin"));

                options.AddPolicy("CanReadPolicy",
                  policy => policy.RequireClaim("Read"));

                options.AddPolicy("CanWritePolicy",
                  policy => policy.RequireUserName("Write"));

                options.AddPolicy("CanDeletePolicy",
                  policy => policy.RequireUserName("Delete"));
            });
            var container = new Container(scope =>
            {
                scope.Scan(x =>
                {
                    x.Assembly("RWEDO.MSQLRepository");
                    x.WithDefaultConventions();

                });
            });
            container.Populate(services);
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
