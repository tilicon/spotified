namespace WhatNext.Web
{
    using AutoMapper;
    using Communication.Web.Contracts.Services;
    using Communication.Web.Services;
    using Communication.Web.Spotify.Contracts.Services;
    using Communication.Web.Spotify.Services;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Music.Contracts.Services;
    using Music.Services;
    using System;

    [UsedImplicitly]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IWebApiAuthorizationService>(new WebApiAuthorizationService(new Uri("https://accounts.spotify.com"), "Basic", "OTk2ZDAwMzc2ODA1NDRjOTg3Mjg3YTliMDQ3MGZkYmI6NWEzYzkyMDk5YTMyNGI4ZjllNDVkNzdlOTE5ZmVjMTM="));
            services.AddSingleton<IWebApiService>(new WebApiService(new Uri("https://api.spotify.com/")));
            services.AddSingleton<ISpotifyService, SpotifyService>();
            services.AddScoped<IMusicService, MusicService>();

            services.AddAutoMapper();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}