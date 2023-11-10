using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Modules.Events;
using AUSBird.DiscordBot.Abstraction.Modules.Interactions;
using AUSBird.DiscordBot.Abstraction.Modules.MessageCommands;
using AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;
using AUSBird.DiscordBot.Abstraction.Modules.UserCommands;
using Microsoft.Extensions.DependencyInjection;

namespace AUSBird.DiscordBot.Abstraction.Extensions;

public static class ModuleExtensions
{
    public static IEnumerable<TCommand> GetModules<TCommand>(this IServiceProvider services)
        where TCommand : IDiscordCommand
    {
        return services.GetServices<TCommand>();
    }

    public static IServiceCollection AddDiscordModule<TModule>(this IServiceCollection collection)
    {
        var interfaces = typeof(TModule).GetInterfaces();

        if (interfaces.Contains(typeof(IDiscordCommand))) AddDiscordCommandModule(collection, typeof(TModule));
        if (interfaces.Contains(typeof(IDiscordEvent))) AddDiscordEventModule(collection, typeof(TModule));
        if (interfaces.Contains(typeof(IDiscordInteraction))) AddDiscordInteractionModule(collection, typeof(TModule));

        collection.AddScoped(typeof(TModule));

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
                if (type == typeof(ISlashCommandAutocomplete))
                    collection.AddScoped(typeof(ISlashCommandAutocomplete), moduleType);

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
            }
    }

    private static void AddDiscordEventModule(IServiceCollection collection, Type moduleType)
    {
        var interfaces = moduleType.GetInterfaces();
        if (interfaces.Contains(typeof(IDiscordEvent)) && moduleType.IsClass)
        {
            collection.AddScoped(typeof(IDiscordEvent), moduleType);

            foreach (var type in interfaces)
            {
                #region Guild Events

                if (type == typeof(IDiscordGuildJoinedEvent))
                    collection.AddScoped(typeof(IDiscordGuildJoinedEvent), moduleType);
                if (type == typeof(IDiscordGuildLeftEvent))
                    collection.AddScoped(typeof(IDiscordGuildLeftEvent), moduleType);
                if (type == typeof(IDiscordGuildUpdatedEvent))
                    collection.AddScoped(typeof(IDiscordGuildUpdatedEvent), moduleType);
                if (type == typeof(IDiscordGuildAvailableEvent))
                    collection.AddScoped(typeof(IDiscordGuildAvailableEvent), moduleType);

                #endregion

                #region Message Events

                if (type == typeof(IDiscordMessageReceivedEvent))
                    collection.AddScoped(typeof(IDiscordMessageReceivedEvent), moduleType);
                if (type == typeof(IDiscordMessageUpdatedEvent))
                    collection.AddScoped(typeof(IDiscordMessageUpdatedEvent), moduleType);
                if (type == typeof(IDiscordMessageDeletedEvent))
                    collection.AddScoped(typeof(IDiscordMessageDeletedEvent), moduleType);

                #endregion

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
                if (type == typeof(IDiscordGuildMemberUpdatedEvent))
                    collection.AddScoped(typeof(IDiscordGuildMemberUpdatedEvent), moduleType);

                #endregion
            }
        }
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