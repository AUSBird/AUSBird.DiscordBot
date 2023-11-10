using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordMessageReceivedEvent : IDiscordEvent
{
    Task HandleDiscordMessageReceivedEvent(SocketMessage message);
}