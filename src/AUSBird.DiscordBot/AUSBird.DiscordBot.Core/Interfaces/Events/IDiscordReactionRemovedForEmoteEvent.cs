using Discord;

namespace AUSBird.DiscordBot.Interfaces.Events;

public interface IDiscordReactionRemovedForEmoteEvent : IDiscordEvent
{
    Task HandleDiscordReactionRemovedForEmoteEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, IEmote emote);
}