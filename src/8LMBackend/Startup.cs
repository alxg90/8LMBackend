﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Http;
using _8LMBackend.Service;
using _8LMBackend.DataAccess.Repositories;
using _8LMBackend.DataAccess.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http.Features;
using System.IO;

namespace _8LMBackend
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials() );
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = 10485760;
                x.MultipartBodyLengthLimit = 10485760;
                x.MultipartHeadersLengthLimit = 10485760;
            });

            // Add framework services.
            services.AddMvc();

            services.AddScoped(typeof(IDbFactory), typeof(DbFactory));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(ICampaignService), typeof(CampaignService));
            services.AddScoped(typeof(ICampaignsRepository), typeof(CampaignsRepository));
			services.AddScoped(typeof(IAccountManagementService), typeof(AccountManagementService));
            services.AddScoped(typeof(IProxyService), typeof(ProxyService));
            services.AddScoped(typeof(IPagesService), typeof(PagesService));
            services.AddScoped(typeof(ISubscribeService), typeof(SubscribeService));
            services.AddScoped(typeof(IFileManagerService), typeof(FileManagerService));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "MyCookieMiddlewareInstance",
                LoginPath = new PathString("/Account/Unauthorized/"),
                AccessDeniedPath = new PathString("/Account/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
               FileProvider = new PhysicalFileProvider(
                     Path.Combine(Directory.GetCurrentDirectory(), @"Content")),
               RequestPath = new PathString("/Content")
            });

            app.UseCors("CorsPolicy");

            app.UseStaticFiles(new StaticFileOptions()
            {
               FileProvider = new PhysicalFileProvider(
                     Path.Combine(Directory.GetCurrentDirectory(), @"Gallery")),
               RequestPath = new PathString("/Gallery")
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
