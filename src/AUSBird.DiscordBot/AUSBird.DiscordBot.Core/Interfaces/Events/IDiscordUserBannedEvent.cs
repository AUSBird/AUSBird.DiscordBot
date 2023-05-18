using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordUserBannedEvent : IDiscordEvent
{
    Task HandleDiscordUserBannedEvent(SocketUser user, SocketGuild guild);
}