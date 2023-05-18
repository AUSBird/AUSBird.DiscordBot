using Discord;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordReactionClearedEvent : IDiscordEvent
{
    Task HandleDiscordReactionClearedEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel);
}