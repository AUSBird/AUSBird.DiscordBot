using AUSBird.DiscordBot.Abstraction.Services;
using AUSBird.DiscordBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUSBird.DiscordBot;

public static class DiscordServiceExtensions
{
    public static IServiceCollection AddDiscordSocketBot(this IServiceCollection collection,
        IConfigurationSection configuration)
    {
        collection.AddHostedService<DiscordHostedService>();
        collection.AddSingleton<IDiscordService, DiscordService>();
        collection.AddSingleton<IDiscordCommandService, DiscordCommandService>();
        collection.AddSingleton<IDiscordEventService, DiscordEventService>();
        collection.AddSingleton<IDiscordInteractionService, DiscordInteractionService>();
        collection.Configure<DiscordServiceConfig>(configuration);
        return collection;
    }
}