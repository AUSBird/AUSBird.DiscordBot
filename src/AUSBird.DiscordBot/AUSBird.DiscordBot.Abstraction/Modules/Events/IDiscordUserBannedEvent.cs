using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordUserBannedEvent : IDiscordEvent
{
    Task HandleDiscordUserBannedEvent(SocketUser user, SocketGuild guild);
}