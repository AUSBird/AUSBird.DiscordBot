using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordRoleCreatedEvent : IDiscordEvent
{
    Task HandleDiscordRoleCreatedEvent(SocketRole role);
}