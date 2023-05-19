using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Services;

public interface IInteractionService
{
    public Task ModalSubmittedAsync(SocketModal modal);
    public Task MessageComponentInteractedAsync(SocketMessageComponent component);
}