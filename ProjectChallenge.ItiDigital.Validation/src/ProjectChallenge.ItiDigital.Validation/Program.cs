using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.AwsCloudWatch;
using Serilog.Sinks.SystemConsole.Themes;
using Amazon.CloudWatchLogs;
using System.IO;
using System.Globalization;
using System.Net;
using Amazon;

namespace ProjectChallenge.ItiDigital.Validation
{
    public class Program
    {
        public static bool IsLocal = false;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog(ConfigureLoggin());
                });

        private static Action<WebHostBuilderContext, LoggerConfiguration> ConfigureLoggin() => (hostingContext, loggerConfiguration) =>
        {
            if (!Enum.TryParse(hostingContext.Configuration["Application:LogLevel"], out LogEventLevel logEventLevel))
            {
                logEventLevel = LogEventLevel.Information;
            }

            var logLevelSwitch = new Serilog.Core.LoggingLevelSwitch(logEventLevel);
            loggerConfiguration
                .MinimumLevel.ControlledBy(logLevelSwitch)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.ApsNetCore.Hosting.Diagnostics", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithDemystifiedStackTraces()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate: "[{Timestamp:yyyy-MM-dd HH.mm.ss.fff zzz} {Level.u3}] [{RequestId}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Logger(lc =>
                {
                    if (!IsLocal)
                    {
                        lc.MinimumLevel.ControlledBy(logLevelSwitch)
                        .Enrich.FromLogContext()
                        .Enrich.WithDemystifiedStackTraces()
                        .WriteTo.AmazonCloudWatch(
                            new CloudWatchSinkOptions { LogGroupName = "ProjectChallenge.ItiDigital.Validation", MinimumLogEventLevel = logEventLevel, TextFormatter = new AWSTextFormatter(), LogStreamNameProvider = new LogStremProvider() },
                            new AmazonCloudWatchLogsClient(RegionEndpoint.GetBySystemName(hostingContext.Configuration["CloudWatchRegion"]))
                        );
                    }
                    else
                    {
                        lc
                        .WriteTo.File("log.txt", outputTemplate: "[{Timestamp:yyyy-MM-dd HH.mm.ss.fff zzz} {Level.u3}] [{RequestId}] {Message:lj}{NewLine}{Exception}");
                    }
                });
        };

        internal class AWSTextFormatter : ITextFormatter
        {
            public void Format(LogEvent logEvent, TextWriter output)
            {
                output.Write($"{logEvent.Timestamp.DateTime.ToISOString()} [{logEvent.Level.ToString()}] [{(logEvent.Properties.ContainsKey("RequestId") ? logEvent.Properties["RequestId"].ToString(): "NORID")}] {logEvent.RenderMessage()}{output.NewLine} ");
                if(logEvent.Exception != null)
                {
                    output.Write(logEvent.Exception);
                }
            }
        }

        internal class LogStremProvider : ILogStreamNameProvider
        {
            public string GetLogStreamName()
            {
                return $"{DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture)}_{Dns.GetHostName()}_{Environment.MachineName}";
            }
        }

    }
}
