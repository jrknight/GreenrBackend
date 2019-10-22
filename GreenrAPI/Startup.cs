using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenrAPI.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace GreenrAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            hostingEnvironment = environment;
            var builder = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath).AddJsonFile("config.json");

            configurationRoot = builder.Build();
            
            Debug.WriteLine("Startup");
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment hostingEnvironment;
        public IConfigurationRoot configurationRoot;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc( option => {
                    option.SslPort = 44364;
                    option.Filters.Add(new RequireHttpsAttribute());
                    })
                .AddMvcOptions( option => {
                    option.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    })
                .AddJsonOptions( option => {
                    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); 
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Creates and configures the role "User" in the database and adds roles
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<GreenrDbContext>();

            //Adds Authentication with Json Web Tokens
            /// TODO: create with OAuth2
            services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(cfg => {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = configurationRoot["Auth:Token:Issuer"],
                    ValidAudience = configurationRoot["Auth:Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationRoot["Auth:GUID"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true

                };
            });

            var testDbConnectionString = configurationRoot["connectionStrings:LocalDb"];

            services.AddDbContext<GreenrDbContext>(options => options.UseSqlServer(testDbConnectionString));

            //services.AddTransient<IConfigurationRoot>();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
