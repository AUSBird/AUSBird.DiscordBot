using AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.CommandAutocomplete;

public interface ICommandAutocomplete : ISlashCommand
{
    public Task ExecuteAutocompleteAsync(SocketAutocompleteInteraction autocomplete);
}