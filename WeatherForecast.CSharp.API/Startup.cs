using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.CSharp.API.Infrastructure;
using WeatherForecast.CSharp.Domain;
using WeatherForecast.CSharp.Authentication;
using WeatherForecast.CSharp.Storage;
using WeatherForecast.CSharp.ForecastProvider;

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
            services.AddSingleton<JwtOptions>()
                .ConfigureStorage(Configuration, "Default")
                .AddCors()
                .AddTransient<IStorageService<Forecast, string>, ForecastStorageService>()
                .AddTransient<IStorageService<User, string>, UserStorageService>()
                .AddTransient<IForecastProvider, WebAPIForecastProvider>()
                .AddTransient<IAuthenticationService, AuthenticationService>()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<IEncryptionService, EncryptionService>()
                .AddTransient<IForecastDeserializer<string>, ForecastJsonDeserializer>()
                .AddTransient<IForecastService, ForecastService>()
                .ConfigureAuthentication()
                .RegisterAutomapper();

            services.AddHttpClient("weather", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("WeatherAPI:BaseUrl"));
            });
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