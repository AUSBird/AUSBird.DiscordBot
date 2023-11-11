using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Modules.Events;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace AUSBird.DiscordBot.Services;

public partial class DiscordHostedService
{
    private void LogEventHandling(string eventName, int handlerCount)
    {
        _logger.LogTrace("Handling event {EvantName} using {HandlerCount} handler(s)", eventName, handlerCount);
    }

    private void LogEventHandlerCalled(string eventName, string handlerName)
    {
        _logger.LogDebug("Calling {HandlerName} to handle {EventName} event", handlerName, eventName);
    }

    private string GetHandlerName(IDiscordEvent eventHandler)
    {
        return eventHandler.GetType().FullName ?? eventHandler.GetType().Name;
    }

    #region Discord Message Events

    private async Task OnMessageReceived(SocketMessage message)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordMessageReceivedEvent>().ToList();
        LogEventHandling(nameof(IDiscordMessageReceivedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordMessageReceivedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordMessageReceivedEvent(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling message received event");
            }
    }

    private async Task OnMessageUpdated(Cacheable<IMessage, ulong> previous, SocketMessage message,
        ISocketMessageChannel channel)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordMessageUpdatedEvent>().ToList();
        LogEventHandling(nameof(IDiscordMessageUpdatedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordMessageUpdatedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordMessageUpdatedEvent(previous, message, channel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling message updated event");
            }
    }

    private async Task OnMessageDeleted(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordMessageDeletedEvent>().ToList();
        LogEventHandling(nameof(IDiscordMessageDeletedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordMessageDeletedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordMessageDeletedEvent(message, channel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling message deleted event");
            }
    }

    #endregion

    #region Discord Guild Events

    private async Task OnJoinedGuild(SocketGuild guild)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordGuildJoinedEvent>().ToList();
        LogEventHandling(nameof(IDiscordGuildJoinedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordGuildJoinedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordGuildJoinedEvent(guild);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling guild joined event");
            }
    }

    private async Task OnLeftGuild(SocketGuild guild)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordGuildLeftEvent>().ToList();
        LogEventHandling(nameof(IDiscordGuildLeftEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordGuildLeftEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordGuildLeftEvent(guild);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling guild left event");
            }
    }

    private async Task OnGuildUpdated(SocketGuild before, SocketGuild after)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordGuildUpdatedEvent>().ToList();
        LogEventHandling(nameof(IDiscordGuildUpdatedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordGuildUpdatedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordGuildUpdatedEvent(before, after);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling guild updated event");
            }
    }

    private async Task OnGuildAvailable(SocketGuild guild)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordGuildAvailableEvent>().ToList();
        LogEventHandling(nameof(IDiscordGuildAvailableEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordGuildAvailableEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordGuildAvailableEvent(guild);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling guild updated event");
            }
    }

    #endregion

    #region Discord Reaction Events

    private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordReactionAddedEvent>().ToList();
        LogEventHandling(nameof(IDiscordReactionAddedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordReactionAddedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordReactionAddedEvent(message, channel, reaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling reaction added event");
            }
    }

    private async Task OnReactionsCleared(Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordReactionClearedEvent>().ToList();
        LogEventHandling(nameof(IDiscordReactionClearedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordReactionClearedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordReactionClearedEvent(message, channel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling reaction cleared event");
            }
    }

    private async Task OnReactionRemoved(Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordReactionRemovedEvent>().ToList();
        LogEventHandling(nameof(IDiscordReactionRemovedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordReactionRemovedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordReactionRemovedEvent(message, channel, reaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling reaction removed event");
            }
    }

    private async Task OnReactionsRemovedForEmote(Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel, IEmote emote)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordReactionRemovedForEmoteEvent>().ToList();
        LogEventHandling(nameof(IDiscordReactionRemovedForEmoteEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordReactionRemovedForEmoteEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordReactionRemovedForEmoteEvent(message, channel, emote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An unhandled exception was encountered while handling reaction removed for emote event");
            }
    }

    #endregion

    #region Discord Role Events

    private async Task OnRoleCreated(SocketRole role)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordRoleCreatedEvent>().ToList();
        LogEventHandling(nameof(IDiscordRoleCreatedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordRoleCreatedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordRoleCreatedEvent(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling role created event");
            }
    }

    private async Task OnRoleDeleted(SocketRole role)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordRoleDeletedEvent>().ToList();
        LogEventHandling(nameof(IDiscordRoleDeletedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordRoleDeletedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordRoleDeletedEvent(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling role deleted event");
            }
    }

    private async Task OnRoleUpdated(SocketRole before, SocketRole after)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordRoleUpdatedEvent>().ToList();
        LogEventHandling(nameof(IDiscordRoleUpdatedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordRoleUpdatedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordRoleUpdatedEvent(before, after);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling role updated event");
            }
    }

    #endregion

    #region Discord User Events

    private async Task OnUserBanned(SocketUser user, SocketGuild guild)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordUserBannedEvent>().ToList();
        LogEventHandling(nameof(IDiscordUserBannedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordUserBannedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordUserBannedEvent(user, guild);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling user banned event");
            }
    }

    private async Task OnUserJoined(SocketGuildUser user)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordUserJoinedEvent>().ToList();
        LogEventHandling(nameof(IDiscordUserJoinedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordUserJoinedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordUserJoinedEvent(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling user banned event");
            }
    }

    private async Task OnUserLeft(SocketGuild user, SocketUser guild)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordUserLeftEvent>().ToList();
        LogEventHandling(nameof(IDiscordUserLeftEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordUserLeftEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordUserLeftEvent(user, guild);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling user left event");
            }
    }

    private async Task OnUserUnbanned(SocketUser user, SocketGuild guild)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordUserUnbannedEvent>().ToList();
        LogEventHandling(nameof(IDiscordUserUnbannedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordUserUnbannedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordUserUnbannedEvent(user, guild);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling user unbanned event");
            }
    }

    private async Task OnUserUpdated(SocketUser before, SocketUser after)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordUserUpdatedEvent>().ToList();
        LogEventHandling(nameof(IDiscordUserUpdatedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordUserUpdatedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordUserUpdatedEvent(before, after);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling user updated event");
            }
    }

    private async Task OnGuildMemberUpdated(Cacheable<SocketGuildUser, ulong> before, SocketGuildUser after)
    {
        var eventHandlers = _eventService.ListEventHandlers<IDiscordGuildMemberUpdatedEvent>().ToList();
        LogEventHandling(nameof(IDiscordGuildMemberUpdatedEvent), eventHandlers.Count);

        foreach (var eventHandler in eventHandlers)
            try
            {
                LogEventHandlerCalled(nameof(IDiscordGuildMemberUpdatedEvent), GetHandlerName(eventHandler));
                await eventHandler.HandleDiscordGuildMemberUpdatedEvent(before, after);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception was encountered while handling user updated event");
            }
    }

    #endregion
}