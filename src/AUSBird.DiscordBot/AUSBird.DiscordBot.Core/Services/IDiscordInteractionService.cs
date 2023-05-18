using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Services;

public interface IDiscordInteractionService
{
    public Task ModalSubmittedAsync(SocketModal modal);
    public Task MessageComponentInteractedAsync(SocketMessageComponent component);
}