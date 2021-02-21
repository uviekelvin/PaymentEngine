using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentEngine.Infrastructure.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentEngine.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: false)
                         .Build();
            AppSettingsDtos appsettings = new AppSettingsDtos();
            config.GetSection("AppSettings").Bind(appsettings);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("'Payment", "Engine")
                .WriteTo.Seq(appsettings.LogUrl)
                .CreateLogger();
            try
            {
                Log.Information("Application Starting Up...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {

                Log.Fatal(e, "Application Failed to StartUp");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
