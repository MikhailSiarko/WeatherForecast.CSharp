﻿using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WeatherForecast.CSharp.API.Database;
using WeatherForecast.CSharp.API.Implementations;
using WeatherForecast.CSharp.API.Infrastructure;
using WeatherForecast.CSharp.API.Interfaces;
using WeatherForecast.CSharp.API.MapperProfiles;

namespace WeatherForecast.CSharp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddDbContextPool<AppDbContext>(builder =>
            {
                builder.UseSqlite(Configuration.GetConnectionString("Default"));
            });
            services.AddCors();
            services.AddHttpClient("weather", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("WeatherAPI:BaseUrl"));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    var jwtOptions = services.BuildServiceProvider(false).GetService<IJwtOptions>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = jwtOptions.SymmetricSecurityKey
                    };
                });
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IForecastDeserializer<string>, ForecastJsonDeserializer>();
            services.AddTransient<IForecastService, ForecastService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>()
                .UseCors(builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                })
                .UseAuthentication()
                .UseHttpsRedirection()
                .UseMvc();
        }
    }
}