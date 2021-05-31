using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentMigrator.Runner;
using System.Reflection;
using Pokok.Migrations;
using Pokok.DataAccess.Migrations;
using Hangfire;
using Microsoft.OpenApi.Models;
using Pokok.Services;
using Pokok.Interfaces;
using Pokok.DataAccess;
using System.IO;

namespace Pokok
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
            services.AddControllersWithViews().AddNewtonsoftJson();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            // Add Fluent Migrator
            services
                .AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .WithGlobalConnectionString(Configuration.GetConnectionString("PokokDB"))
                    .AddSqlServer()
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All());
            // Add access to configuration
            services.AddSingleton<IConfiguration>(Configuration);
            // Add background job instance
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("PokokDB")));
            services.AddHangfireServer();
            // Add swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tree API",
                    Version = "v1"
                });
            });
            // register additional services
            services.AddScoped<IDataAccess, SqlDataAccess>();
            services.AddScoped<ITreeService, TreeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                // Start swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tree API");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "ClientApp");

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            // Start database migration
            Database.EnsureDatabase(Configuration.GetConnectionString("PokokDB"), "PokokDB");
            app.Migrate();
        }
    }
}
