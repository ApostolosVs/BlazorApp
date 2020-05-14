using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Controllers;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddMvc();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            //Service for the Mongo Database!!
            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IMongoDbSettings>(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            //The interface ICustomerService describe the services for the customers!!
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddSingleton<CustomerService>();

            services.AddControllers();
            //The service which handle the authentication from IdentityServer4 demo.
            //In our case we use the OpenId to connect users in our app.
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            })
              .AddCookie("cookie")
              .AddOpenIdConnect("oidc", options =>
              {
                  options.Authority = "https://demo.identityserver.io"; //The authority is the url of the identity server 4
                  //The below values contained in the identity server 4 demo site.
                  options.ClientId = "interactive.confidential";
                  options.ClientSecret = "secret";

                  options.ResponseType = "code";
                  options.GetClaimsFromUserInfoEndpoint = true;
                  options.SaveTokens = true;

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      NameClaimType = "name"
                  };
              });
       
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            

            app.UseRouting();
            //For the authentcation and authorization by identity server 4
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
        }
    }
}
