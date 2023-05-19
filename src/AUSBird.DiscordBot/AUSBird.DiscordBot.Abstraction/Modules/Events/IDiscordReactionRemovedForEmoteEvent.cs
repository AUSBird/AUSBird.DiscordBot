using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.Events;

public interface IDiscordReactionRemovedForEmoteEvent : IDiscordEvent
{
    Task HandleDiscordReactionRemovedForEmoteEvent(Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel, IEmote emote);
}