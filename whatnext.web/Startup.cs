namespace WhatNext.Web
{
    using AutoMapper;
    using Communication.Web.Contracts.Services;
    using Communication.Web.Spotify.Contracts.Services;
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
    using System.Net.Http;
    using Communication.Web.Services;
    using Communication.Web.Spotify.Services;

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

            var uriApi = new Uri(Configuration.GetSection("Spotify:ApiUri").Value);
            var uriAccounts = Configuration.GetSection("Spotify:AccountsUri").Value;
            var tokenPath = Configuration.GetSection("Spotify:tokenPath").Value;
            var token = Configuration.GetSection("Spotify:token").Value;

            var authorizationClient = new HttpClient
            {
                BaseAddress = new Uri(uriAccounts),
            };
            var spotifyClientHandler = new SpotifyClientHandler(authorizationClient, tokenPath, "Basic", token);
            var httpClient = new HttpClient(spotifyClientHandler)
            {
                BaseAddress = uriApi,
            };

            services.AddSingleton<IWebApiService>(new WebApiService(uriApi, httpClient));
            services.AddSingleton<ISpotifyService, SpotifyService>();
            services.AddScoped<IMusicService, MusicService>();

            services.AddAutoMapper(typeof(IMusicService), typeof(ISpotifyService), typeof(Startup));

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
                var errorPath = Configuration.GetSection("Api:ErrorPath").Value;
                app.UseExceptionHandler(errorPath);
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