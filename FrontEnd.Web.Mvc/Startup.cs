using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Abstraction;
using BackEnd.Helper;
using BackEnd.Services;
using FrontEnd.Web.Mvc.Models.Admin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FrontEnd.Web.Mvc
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Auth/LoginCalonSiswa");
                    options.AccessDeniedPath = new PathString("/Error/403");
                });
            services.AddControllersWithViews();
            services.AddSingleton(Configuration);
            services.AddScoped<IDbConnectionHelper, DbConnectionHelper>(
                _ => new DbConnectionHelper(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ISecurityRelate, SecurityRelateHelper>();
            services.AddScoped<IStaffSma, StaffSmaService>();
            services.AddScoped<ISoalPenerimaan, SoalPenerimaanService>();
            services.AddScoped<IPendaftaran, PendaftaranService>();
            services.AddScoped<ICalonSiswa, CalonSiswaService>();
            services.AddScoped<IUjian, UjianService>();
            services.AddScoped<ISeleksiPenerimaan, SeleksiPenerimaanService>();
            services.AddScoped<ISiswa, SiswaService>();
            services.AddScoped<IKelas, KelasService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/404");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Index}/{id?}");
            });
        }
    }
}
