using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using IdentityApi.Data;
//using IdentityApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;

namespace ResourceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host =CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(loging =>
                {
                    loging.ClearProviders();
                    loging.SetMinimumLevel(LogLevel.Trace);
                }).UseNLog();
    }
}
