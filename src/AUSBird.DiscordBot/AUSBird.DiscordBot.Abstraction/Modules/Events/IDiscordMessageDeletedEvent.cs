using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordMessageDeletedEvent : IDiscordEvent
{
    Task HandleDiscordMessageDeletedEvent(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel);
}