using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordRoleDeletedEvent : IDiscordEvent
{
    Task HandleDiscordRoleDeletedEvent(SocketRole role);
}