using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using TransportManagmentSystemBackend.Api;

namespace TransportManagmentSystemBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, reloadOnChange: true);

            if (currentEnvironment == Environments.Development)
            {
                configBuilder.AddJsonFile($"appsettings.{currentEnvironment}.json", optional: false);
            }
            else
            {
                configBuilder.AddJsonFile("appsettings.json", optional: false);
            }

            IConfigurationRoot config = configBuilder.Build();
            LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {
                logger.Info($"{ApiConstants.ServiceName} starts running...");

                CreateWebHostBuilder(args, config).Build().Run();

                logger.Info($"{ApiConstants.TransportManagmentSystemName} is stopped");
            }
            catch (Exception exception)
            {
                logger.Error(exception);

                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot configuration)
        {
            return Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((context, config) =>
                 {
                     var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
                 })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddAzureWebAppDiagnostics();
                    logging.AddConsole();
                    //logging.AddEventLog();
                })
            .UseNLog());
        }
    }
}
