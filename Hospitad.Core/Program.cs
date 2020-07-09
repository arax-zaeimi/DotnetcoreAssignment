using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Hospitad.Api
{
    public class Program
    {
        //read namespace of project
        public static readonly string Namespace = typeof(Program).Namespace;
        //read project name
        public static readonly string AppName = Namespace ?? Namespace?.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
        /// <summary>
        /// make configuration reading better
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();


            try
            {
                //log config host
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);

                CreateHostBuilder(args).Build().Run();
                //log app start
                Log.Information("Starting web host ({ApplicationContext})...", AppName);

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseConfiguration(Configuration)
            .UseStartup<Startup>()
            .ConfigureLogging(loggingConfiguration => loggingConfiguration.ClearProviders())
            .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom
            .Configuration(hostingContext.Configuration));
    }
}
