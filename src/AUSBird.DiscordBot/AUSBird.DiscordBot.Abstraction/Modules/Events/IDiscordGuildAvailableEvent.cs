using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordGuildAvailableEvent : IDiscordEvent
{
    Task HandleDiscordGuildAvailableEvent(SocketGuild guild);
}