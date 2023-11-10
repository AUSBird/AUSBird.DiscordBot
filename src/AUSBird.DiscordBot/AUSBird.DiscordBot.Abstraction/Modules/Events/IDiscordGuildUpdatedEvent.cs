using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordGuildUpdatedEvent : IDiscordEvent
{
    Task HandleDiscordGuildUpdatedEvent(SocketGuild before, SocketGuild after);
}