using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;

public interface IGuildSlashCommand : ISlashCommand, IDiscordGuildCommand
{
    public SlashCommandBuilder BuildGuildSlashCommand(string name);
}