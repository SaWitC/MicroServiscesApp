using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;

namespace IdentityApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build();

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var UserManager = services.GetRequiredService<UserManager<User>>();
                    var RoleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await SampleData.Initialize(UserManager, RoleManager);
                }
                catch
                {

                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(res =>
                {
                    res.ClearProviders();
                    res.SetMinimumLevel(LogLevel.Trace);
                }).UseNLog();
    }
}
