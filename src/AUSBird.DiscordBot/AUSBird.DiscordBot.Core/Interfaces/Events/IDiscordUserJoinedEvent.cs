using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordUserJoinedEvent : IDiscordEvent
{
    Task HandleDiscordUserJoinedEvent(SocketGuildUser user);
}