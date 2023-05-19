using AUSBird.DiscordBot;
using AUSBird.DiscordBot.Abstraction.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace TestBot
{
    public static class Program
    {
        public static IHostBuilder CreateHost() => new HostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddEnvironmentVariables("TEST__");

                // Configuration Files
                builder.AddJsonFile("settings.json", true, true);
            })
            .UseSerilog((builderContext, loggerConfiguration) =>
            {
                loggerConfiguration.Enrich.FromLogContext();
                loggerConfiguration.Enrich.WithCorrelationId();
                loggerConfiguration.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}");
                //loggerConfiguration.WriteTo.Console(new JsonFormatter(renderMessage: true));
                loggerConfiguration.ReadFrom.Configuration(builderContext.Configuration);
            })
            .UseDefaultServiceProvider((context, options) =>
            {
                bool isDevelopment = context.HostingEnvironment.IsDevelopment();
                options.ValidateScopes = isDevelopment;
                options.ValidateOnBuild = isDevelopment;
            })
            .ConfigureServices((context, collection) =>
            {
                collection.AddDiscordSocketBot(context.Configuration.GetSection("Discord"));
                collection.AddHealthChecks();

                collection.AddDiscordModule<TestModule>();
            });

        public static void Main(string[] args)
        {
            CreateHost().Build().Run();
        }
    }
}