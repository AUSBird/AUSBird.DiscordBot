using AUSBird.DiscordBot.Interfaces;
using AUSBird.DiscordBot.Interfaces.CommandAutocomplete;
using AUSBird.DiscordBot.Interfaces.Events;
using AUSBird.DiscordBot.Interfaces.Interactions;
using AUSBird.DiscordBot.Interfaces.MessageCommands;
using AUSBird.DiscordBot.Interfaces.SlashCommands;
using AUSBird.DiscordBot.Interfaces.UserCommands;
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

    public static IServiceCollection AddDiscordModule<TModule>(this IServiceCollection collection)
    {
        var interfaces = typeof(TModule).GetInterfaces();

        if (interfaces.Contains(typeof(IDiscordCommand))) AddDiscordCommandModule(collection, typeof(TModule));
        if (interfaces.Contains(typeof(IDiscordEvent))) AddDiscordEventModule(collection, typeof(TModule));
        if (interfaces.Contains(typeof(IDiscordInteraction))) AddDiscordInteractionModule(collection, typeof(TModule));
        return collection;
    }

    public static IServiceCollection AddDiscordCommandModule<TCommandModule>(this IServiceCollection collection)
        where TCommandModule : class, IDiscordCommand
    {
        AddDiscordCommandModule(collection, typeof(TCommandModule));
        return collection;
    }

    private static void AddDiscordCommandModule(IServiceCollection collection, Type moduleType)
    {
        var interfaces = moduleType.GetInterfaces();
        if (interfaces.Contains(typeof(IDiscordCommand)) && moduleType.IsClass)
            foreach (var type in interfaces)
            {
                #region Base Interfaces

                if (type == typeof(IDiscordCommand))
                    collection.AddScoped(typeof(IDiscordCommand), moduleType);
                if (type == typeof(IDiscordGlobalCommand))
                    collection.AddScoped(typeof(IDiscordGlobalCommand), moduleType);
                if (type == typeof(IDiscordGuildCommand))
                    collection.AddScoped(typeof(IDiscordGuildCommand), moduleType);

                #endregion

                #region Slash Commands

                if (type == typeof(ISlashCommand))
                    collection.AddScoped(typeof(ISlashCommand), moduleType);
                if (type == typeof(IGlobalSlashCommand))
                    collection.AddScoped(typeof(IGlobalSlashCommand), moduleType);
                if (type == typeof(IGuildSlashCommand))
                    collection.AddScoped(typeof(IGuildSlashCommand), moduleType);

                #endregion

                #region User Commands

                if (type == typeof(IUserCommand))
                    collection.AddScoped(typeof(IUserCommand), moduleType);
                if (type == typeof(IGlobalUserCommand))
                    collection.AddScoped(typeof(IGlobalUserCommand), moduleType);
                if (type == typeof(IGuildUserCommand))
                    collection.AddScoped(typeof(IGuildUserCommand), moduleType);

                #endregion

                #region Message Commands

                if (type == typeof(IMessageCommand))
                    collection.AddScoped(typeof(IMessageCommand), moduleType);
                if (type == typeof(IGlobalMessageCommand))
                    collection.AddScoped(typeof(IGlobalMessageCommand), moduleType);
                if (type == typeof(IGuildMessageCommand))
                    collection.AddScoped(typeof(IGuildMessageCommand), moduleType);

                #endregion

                #region Autocomplete

                if (type == typeof(ICommandAutocomplete))
                    collection.AddScoped(typeof(ICommandAutocomplete), moduleType);
                if (type == typeof(IGlobalCommandAutocomplete))
                    collection.AddScoped(typeof(IGlobalCommandAutocomplete), moduleType);
                if (type == typeof(IGuildCommandAutocomplete))
                    collection.AddScoped(typeof(IGuildCommandAutocomplete), moduleType);

                #endregion
            }
    }

    public static IServiceCollection AddDiscordEventModule<TEventModule>(this IServiceCollection collection)
        where TEventModule : class, IDiscordEvent
    {
        AddDiscordEventModule(collection, typeof(TEventModule));
        return collection;
    }

    private static void AddDiscordEventModule(IServiceCollection collection, Type moduleType)
    {
        var interfaces = moduleType.GetInterfaces();
        if (interfaces.Contains(typeof(IDiscordEvent)) && moduleType.IsClass)
        {
            collection.AddScoped(typeof(IDiscordEvent), moduleType);

            foreach (var type in interfaces)
            {
                #region Reaction Events

                if (type == typeof(IDiscordReactionAddedEvent))
                    collection.AddScoped(typeof(IDiscordReactionAddedEvent), moduleType);
                if (type == typeof(IDiscordReactionClearedEvent))
                    collection.AddScoped(typeof(IDiscordReactionClearedEvent), moduleType);
                if (type == typeof(IDiscordReactionRemovedEvent))
                    collection.AddScoped(typeof(IDiscordReactionRemovedEvent), moduleType);
                if (type == typeof(IDiscordReactionRemovedForEmoteEvent))
                    collection.AddScoped(typeof(IDiscordReactionRemovedForEmoteEvent), moduleType);

                #endregion

                #region Role Events

                if (type == typeof(IDiscordRoleCreatedEvent))
                    collection.AddScoped(typeof(IDiscordRoleCreatedEvent), moduleType);
                if (type == typeof(IDiscordRoleDeletedEvent))
                    collection.AddScoped(typeof(IDiscordRoleDeletedEvent), moduleType);
                if (type == typeof(IDiscordRoleUpdatedEvent))
                    collection.AddScoped(typeof(IDiscordRoleUpdatedEvent), moduleType);

                #endregion

                #region User Events

                if (type == typeof(IDiscordUserBannedEvent))
                    collection.AddScoped(typeof(IDiscordUserBannedEvent), moduleType);
                if (type == typeof(IDiscordUserJoinedEvent))
                    collection.AddScoped(typeof(IDiscordUserJoinedEvent), moduleType);
                if (type == typeof(IDiscordUserLeftEvent))
                    collection.AddScoped(typeof(IDiscordUserLeftEvent), moduleType);
                if (type == typeof(IDiscordUserUnbannedEvent))
                    collection.AddScoped(typeof(IDiscordUserUnbannedEvent), moduleType);
                if (type == typeof(IDiscordUserUpdatedEvent))
                    collection.AddScoped(typeof(IDiscordUserUpdatedEvent), moduleType);

                #endregion
            }
        }
    }

    public static IServiceCollection AddDiscordInteractionModule<TInteractionModule>(this IServiceCollection collection)
        where TInteractionModule : class, IDiscordInteraction
    {
        AddDiscordInteractionModule(collection, typeof(TInteractionModule));
        return collection;
    }

    private static void AddDiscordInteractionModule(IServiceCollection collection, Type moduleType)
    {
        var interfaces = moduleType.GetInterfaces();
        collection.AddScoped(typeof(IDiscordInteraction), moduleType);

        foreach (var type in interfaces)
        {
            if (type == typeof(IDiscordModalSubmit))
                collection.AddScoped(typeof(IDiscordModalSubmit), moduleType);
            if (type == typeof(IDiscordComponentInteraction))
                collection.AddScoped(typeof(IDiscordComponentInteraction), moduleType);
        }
    }
}