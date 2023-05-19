using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Services;

public interface IDiscordCommandService
{
    public IEnumerable<ApplicationCommandProperties> BuildGlobalCommandsModulesAsync();
    
    public Task ExecuteSlashCommandAsync(SocketSlashCommand socketCommand);
    public Task ExecuteUserCommandAsync(SocketUserCommand socketCommand);
    public Task ExecuteMessageCommandAsync(SocketMessageCommand socketCommand);
    public Task ExecuteAutocompleteAsync(SocketAutocompleteInteraction autocomplete);
}