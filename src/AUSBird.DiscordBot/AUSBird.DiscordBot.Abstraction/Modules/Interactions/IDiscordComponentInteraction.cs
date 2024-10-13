using System.Text.RegularExpressions;
using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.Interactions;

public interface IDiscordComponentInteraction : IDiscordInteraction
{
    public Regex[] ComponentIds { get; }
    public Task ExecuteInteractionAsync(IComponentInteraction component);
}