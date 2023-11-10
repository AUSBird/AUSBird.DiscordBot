using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordGuildMemberUpdatedEvent : IDiscordEvent
{
    Task HandleDiscordGuildMemberUpdatedEvent(Cacheable<SocketGuildUser, ulong> before, SocketGuildUser after);
}