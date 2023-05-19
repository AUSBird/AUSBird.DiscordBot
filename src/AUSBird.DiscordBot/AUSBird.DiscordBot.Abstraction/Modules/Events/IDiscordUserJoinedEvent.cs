using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordUserJoinedEvent : IDiscordEvent
{
    Task HandleDiscordUserJoinedEvent(SocketGuildUser user);
}