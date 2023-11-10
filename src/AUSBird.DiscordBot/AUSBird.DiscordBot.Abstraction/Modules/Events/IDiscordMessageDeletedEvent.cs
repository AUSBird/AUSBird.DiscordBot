using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordMessageDeletedEvent : IDiscordEvent
{
    Task HandleDiscordMessageReceivedEvent(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel);
}