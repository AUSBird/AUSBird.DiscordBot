using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordRoleCreatedEvent : IDiscordEvent
{
    Task HandleDiscordRoleCreatedEvent(SocketRole role);
}