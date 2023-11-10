using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordMessageUpdatedEvent : IDiscordEvent
{
    Task HandleDiscordMessageUpdatedEvent(Cacheable<IMessage, ulong> previous, SocketMessage message,
        ISocketMessageChannel channel);
}