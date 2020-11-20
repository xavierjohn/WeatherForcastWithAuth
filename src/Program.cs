using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Reflection;

namespace Weather
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            SetupLogging();

            try
            {
                Log.Information("**** Starting WebHost ****");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void SetupLogging()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            string logFile = $"{System.IO.Path.GetTempPath()}Weather/WeatherAPI.json";
            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                                .Enrich.FromLogContext()
                                .Enrich.WithMachineName()
                                .Enrich.WithProperty("Assembly", assembly.Name)
                                .Enrich.WithProperty("Version", assembly.Version)
                                .WriteTo.Trace()
                                .WriteTo.Console()
                                .WriteTo.File(new CompactJsonFormatter(), logFile)
                                .CreateLogger(); ;
            Log.Information("Logging to file {filename}", logFile);
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
