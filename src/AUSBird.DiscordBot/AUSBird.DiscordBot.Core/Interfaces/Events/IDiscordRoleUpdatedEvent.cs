using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordRoleUpdatedEvent : IDiscordEvent
{
    Task HandleDiscordRoleUpdatedEvent(SocketRole before, SocketRole after);
}