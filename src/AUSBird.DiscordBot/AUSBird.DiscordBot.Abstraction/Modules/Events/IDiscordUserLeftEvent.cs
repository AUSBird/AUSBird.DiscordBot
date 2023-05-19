using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordUserLeftEvent : IDiscordEvent
{
    Task HandleDiscordUserLeftEvent(SocketGuild user, SocketUser guild);
}