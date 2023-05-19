using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.UserCommands;

public interface IGuildUserCommand : IUserCommand, IDiscordGuildCommand
{
    public UserCommandBuilder BuildGuildUserCommand(string name);
}