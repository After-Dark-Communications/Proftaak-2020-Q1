using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApplication1.Services;
using DAL.Interfaces;
using DAL.Concrete;
using DAL.Context;
using Logic;
using Microsoft.AspNetCore.Http;
using WebApplication1.Repository;

namespace WebApplication1
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
            DBConnection._connectionString = (Configuration.GetConnectionString("DefaultConnection"));
            services.AddControllersWithViews();

            services.AddDbContext<DepotContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddAutoMapper(typeof(MappingBootstrapper));
            services.AddScoped<ITramAccess, TramAccess>();
            services.AddScoped<ISectorAccess, SectorAccess>();
            services.AddScoped<ITrackAccess, TrackAccess>();
            services.AddScoped<IUserAccess, UserAccess>();
            services.AddScoped<Tram>();
            services.AddScoped<Track>();
            services.AddScoped<Sector>();
            services.AddScoped<Depot>();
            services.AddScoped<User>();
            services.AddScoped<LoginRepository>();
            services.AddScoped<UserCollection>();
            services.AddScoped<IDepotAccess, DepotAccess>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
