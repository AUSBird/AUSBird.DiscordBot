using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.Interactions;

public interface IDiscordModalSubmit : IDiscordInteraction
{
    public string ModalId { get; }
    public Task ModalSubmittedAsync(SocketModal modal);
    public ModalBuilder BuildModal();
}