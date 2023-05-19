using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.UserCommands;

public interface IGlobalUserCommand : IUserCommand, IDiscordGlobalCommand
{
    public UserCommandBuilder BuildGlobalUserCommand();
}