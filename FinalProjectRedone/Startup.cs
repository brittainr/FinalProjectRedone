using FinalProjectRedone.Models;
using FinalProjectRedone.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace FinalProjectRedone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IFinance, FinanceRepo>();
            services.AddDbContext<TaxContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
            services.AddIdentity<UserModel, IdentityRole>().AddEntityFrameworkStores<TaxContext>().AddDefaultTokenProviders();
     
        }

      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TaxContext>();

                //context.Database.Migrate();
                var userManager = scope.ServiceProvider.GetService(typeof(UserManager<UserModel>));
                var roleManager = scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
                //SeedData.Init(context, (UserManager<UserModel>)userManager, (RoleManager<IdentityRole>)roleManager);
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
