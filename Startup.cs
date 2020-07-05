using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DoceGlamourCore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.CodeAnalysis.Options;
using DoceGlamourCore.Sessao;
using DoceGlamourCore.Libraries.LoginUser;

namespace doceglamour
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string UsuarioDB { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;
               

            });

            
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession(op => op.IdleTimeout = TimeSpan.FromMinutes(20));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddScoped<Sessao>();
            services.AddScoped<LoginUser>();
            services.AddEntityFrameworkNpgsql().AddDbContext<UsuarioContext>(options => options.UseNpgsql(Configuration.GetConnectionString("UsuarioDB")));
            services.AddEntityFrameworkNpgsql().AddDbContext<ClienteContext>(options => options.UseNpgsql(Configuration.GetConnectionString("UsuarioDB")));
            services.AddEntityFrameworkNpgsql().AddDbContext<ProdutoContext>(options => options.UseNpgsql(Configuration.GetConnectionString("UsuarioDB")));
            services.AddEntityFrameworkNpgsql().AddDbContext<PedidoContext>(options => options.UseNpgsql(Configuration.GetConnectionString("UsuarioDB")));
            services.AddEntityFrameworkNpgsql().AddDbContext<ProdutoPedidoContext>(options => options.UseNpgsql(Configuration.GetConnectionString("UsuarioDB")));
            
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

            app.UseRouting();

            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
         

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
