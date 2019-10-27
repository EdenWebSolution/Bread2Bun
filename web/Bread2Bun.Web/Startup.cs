using AutoMapper;
using Bread2Bun.Data;
using Bread2Bun.Domain.Security;
using Bread2Bun.Domain.Security.TokenProviders;
using Bread2Bun.Service;
using Bread2Bun.Web.AppHubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddAutoMapper(new MappingProfile().GetType().Assembly);
            ServiceInjector.InjectServices(services);

            services.AddIdentity<StoreUser, StoreUserRole>(cfg =>
            {
                // user the below options to make the email unique
                cfg.SignIn.RequireConfirmedEmail = true;
                cfg.User.RequireUniqueEmail = true;
                cfg.Lockout.AllowedForNewUsers = false;
                //setup email confirmation token provider
                cfg.Tokens.EmailConfirmationTokenProvider = "emailconf";

                // user the below options to make the password policy
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredLength = 6;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<Bread2BunContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<StoreUser>>("emailconf");

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(30);
            });


            // to override DataProtectionTokenProviderOptions token life span
            services.Configure<EmailConfirmationTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(30);
            });


            services.AddAuthentication()
              .AddJwtBearer(cfg =>
              {
                  cfg.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidIssuer = configuration["Tokens:Issuer"],
                      ValidAudience = configuration["Tokens:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]))
                  };

                  cfg.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          var accessToken = context.Request.Query["access_token"];

                          // If the request is for our hub...
                          var path = context.HttpContext.Request.Path;
                          if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
                          {
                              // Read the token out of the query string
                              context.Token = accessToken;
                          }
                          return Task.CompletedTask;
                      }
                  };
              });

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.SignIn.RequireConfirmedEmail = true;
            //});

            services.AddDbContext<Bread2BunContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("B2BContext"));
            });

            services.AddSignalR();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bread2Bun",
                    Version = "v1"
                });
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Bread2BunUI/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            //app.UseCors(builder => builder.WithOrigins("https://www.bread2bun.com/", "http://bread2bun.azurewebsites.net", "http://www.bread2bun.com", "https://bread2bun.azurewebsites.net", "http://localhost:4200", "http://localhost:54969").AllowAnyHeader().AllowAnyMethod());
            app.UseCors(builder => builder.WithOrigins("*", "http://localhost:4200", "http://localhost:54969", "http://test.bread2bun.com", "https://www.bread2bun.com").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bread2Bun API V1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "Bread2BunUI";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
