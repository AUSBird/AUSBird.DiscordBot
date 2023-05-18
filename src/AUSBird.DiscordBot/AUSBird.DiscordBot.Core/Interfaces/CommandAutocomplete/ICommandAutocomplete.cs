using AUSBird.DiscordBot.Interfaces.SlashCommands;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.CommandAutocomplete;

public interface ICommandAutocomplete : ISlashCommand
{
    public Task ExecuteAutocompleteAsync(SocketAutocompleteInteraction autocomplete);
}