using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordGuildLeftEvent : IDiscordEvent
{
    Task HandleDiscordGuildLeftEvent(SocketGuild guild);
}