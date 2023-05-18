using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordUserUnbannedEvent : IDiscordEvent
{
    Task HandleDiscordUserUnbannedEvent(SocketUser user, SocketGuild guild);
}