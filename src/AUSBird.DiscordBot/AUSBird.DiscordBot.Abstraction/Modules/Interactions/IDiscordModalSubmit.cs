using System.Text.RegularExpressions;
using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.Interactions;

public interface IDiscordModalSubmit : IDiscordInteraction
{
    public Regex[] ModalIds { get; }
    public Task ModalSubmittedAsync(SocketModal modal);
    public ModalBuilder BuildModal();
}