using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordRoleUpdatedEvent : IDiscordEvent
{
    Task HandleDiscordRoleUpdatedEvent(SocketRole before, SocketRole after);
}