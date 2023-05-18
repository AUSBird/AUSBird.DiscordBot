using Discord;

namespace AUSBird.DiscordBot.Interfaces.Interactions;

public interface IDiscordComponentInteraction : IDiscordInteraction
{
    public string[] ComponentIds { get; }
    public Task ExecuteInteractionAsync(IComponentInteraction modal);
}