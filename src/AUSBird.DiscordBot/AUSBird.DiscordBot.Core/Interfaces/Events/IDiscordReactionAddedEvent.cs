using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordReactionAddedEvent : IDiscordEvent
{
    Task HandleDiscordReactionAddedEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel,
        SocketReaction reaction);
}