using AUSBird.DiscordBot.Interfaces;
using AUSBird.DiscordBot.Interfaces.Events;
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

    #endregion
}