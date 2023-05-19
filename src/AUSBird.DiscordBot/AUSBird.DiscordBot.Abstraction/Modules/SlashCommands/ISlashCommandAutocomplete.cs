using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;

public interface ISlashCommandAutocomplete : ISlashCommand
{
    public Task ExecuteAutocompleteAsync(SocketAutocompleteInteraction autocomplete);
}