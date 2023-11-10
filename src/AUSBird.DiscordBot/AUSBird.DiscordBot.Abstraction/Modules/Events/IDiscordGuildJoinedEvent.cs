using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordGuildJoinedEvent : IDiscordEvent
{
    Task HandleDiscordGuildJoinedEvent(SocketGuild guild);
}