using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordRoleDeletedEvent : IDiscordEvent
{
    Task HandleDiscordRoleDeletedEvent(SocketRole role);
}