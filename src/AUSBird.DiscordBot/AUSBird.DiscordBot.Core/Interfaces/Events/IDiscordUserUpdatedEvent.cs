using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordUserUpdatedEvent : IDiscordEvent
{
    Task HandleDiscordUserUpdatedEvent(SocketUser before, SocketUser after);
}