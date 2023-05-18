using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Services;

public interface IDiscordCommandService
{
    public Task<IEnumerable<ApplicationCommandProperties>> BuildCommandsModulesAsync();
    public Task ExecuteSlashCommandAsync(SocketSlashCommand socketCommand);
    public Task ExecuteUserCommandAsync(SocketUserCommand socketCommand);
    public Task ExecuteMessageCommandAsync(SocketMessageCommand socketCommand);
    public Task ExecuteAutocompleteAsync(SocketAutocompleteInteraction autocomplete);

    public IEnumerable<string> ListSlashCommandIds();
    public IEnumerable<string> ListUserCommandIds();
    public IEnumerable<string> ListMessageCommandIds();
}