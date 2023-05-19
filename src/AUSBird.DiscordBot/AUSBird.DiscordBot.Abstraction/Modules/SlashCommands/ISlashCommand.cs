using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;

public interface ISlashCommand : IDiscordCommand
{
    public Task ExecuteSlashCommandAsync(SocketSlashCommand command);
}