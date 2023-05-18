using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordReactionRemovedEvent : IDiscordEvent
{
    Task HandleDiscordReactionRemovedEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction);
}