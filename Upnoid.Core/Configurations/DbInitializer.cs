using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Upnoid.Core.Data;
using Upnoid.Domain.Models;

namespace Upnoid.Core.Configurations
{
    public static class DbInitializer
    {
        public static UpnoidContext _context { get; set; }
        //seed database
        public static void SeedData(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                string[] arg = new string[2];
                _context = GetContext(app, arg);
                _context.Database.EnsureCreated();
                Task.Run(() => SeedUserRoles(app))
                    .ContinueWith((t)=>SeedAdminUser(app));
            }
        }

        //Get DbContext
        public static UpnoidContext GetContext(IApplicationBuilder app,string[] options2)
        {
            UpnoidContextFactory p = new UpnoidContextFactory();
            return p.CreateDbContext(options2);
        }

        //Add Default User
        public static async Task SeedAdminUser(IApplicationBuilder app)
        {
            IServiceScopeFactory scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                //string[] options = new string[2];//only used to instatiate context
                //var db = GetContext(app, options);
                UpnoidContext db = scope.ServiceProvider.GetService<UpnoidContext>();
                if (db.Users.Count()==0 || db.Users.FirstOrDefault(p => p.UserName == "MasterCoder") == null)
                {
                    var user = new ApplicationUser { UserName = "xabisohila@gmail.com", Email = "xabisohila@gmail.com", EmailConfirmed=true};
                    string password = "@Password1";
                    var result = await userManager.CreateAsync(user, password);
                    if(result.Succeeded)
                    {
                        var result1 = await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }

        //Add User Roles
        public static async Task SeedUserRoles(IApplicationBuilder app)
        {
            IServiceScopeFactory scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                }
                if (!await roleManager.RoleExistsAsync("Customer"))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Customer" });
                }
            }
        }
    }
}







