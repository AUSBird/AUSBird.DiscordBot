using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.Interactions;

public interface IDiscordComponentInteraction : IDiscordInteraction
{
    public string[] ComponentIds { get; }
    public Task ExecuteInteractionAsync(IComponentInteraction component);
}