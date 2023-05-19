using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;

public interface ISlashCommand : IDiscordCommand
{
    string SlashCommandId { get; }
    public Task ExecuteSlashCommandAsync(SocketSlashCommand command);
    public SlashCommandBuilder BuildSlashCommand();
}