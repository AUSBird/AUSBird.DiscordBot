using AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;
using Discord;
using Discord.WebSocket;

namespace TestBot;

public class TestModule : IGlobalSlashCommand
{
    public async Task ExecuteSlashCommandAsync(SocketSlashCommand command)
    {
        await command.RespondAsync("Hello, world");
    }

    public SlashCommandBuilder BuildGlobalSlashCommand() => new SlashCommandBuilder()
        .WithName("test").WithDescription("Test command")
        .WithDefaultPermission(true)
        .WithDMPermission(true);
}