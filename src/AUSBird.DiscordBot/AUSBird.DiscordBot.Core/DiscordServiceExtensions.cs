using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Services;
using AUSBird.DiscordBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUSBird.DiscordBot;

public static class DiscordServiceExtensions
{
    public static IServiceCollection AddDiscordSocketBot(this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection.AddHostedService<DiscordHostedService>();
        collection.AddSingleton<IDiscordService, DiscordService>();
        collection.AddSingleton<ICommandService, CommandService>();
        collection.AddSingleton<IEventService, EventService>();
        collection.AddSingleton<IInteractionService, InteractionService>();
        collection.AddSingleton<ICommandModuleMapper, CommandModuleMapper>();
        collection.Configure<DiscordServiceConfig>(configuration);
        return collection;
    }
}