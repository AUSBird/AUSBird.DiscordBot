using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordUserUpdatedEvent : IDiscordEvent
{
    Task HandleDiscordUserUpdatedEvent(SocketUser before, SocketUser after);
}