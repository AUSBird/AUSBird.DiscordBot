using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;

public interface IGlobalSlashCommand : ISlashCommand, IDiscordGlobalCommand
{
    public SlashCommandBuilder BuildGlobalSlashCommand();
}