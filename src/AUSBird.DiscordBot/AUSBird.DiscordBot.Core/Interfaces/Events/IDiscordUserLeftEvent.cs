using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordUserLeftEvent : IDiscordEvent
{
    Task HandleDiscordUserLeftEvent(SocketGuild user, SocketUser guild);
}