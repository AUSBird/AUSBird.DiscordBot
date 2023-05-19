using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordUserUnbannedEvent : IDiscordEvent
{
    Task HandleDiscordUserUnbannedEvent(SocketUser user, SocketGuild guild);
}