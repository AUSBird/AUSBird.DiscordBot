using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.UserCommands;

public interface IUserCommand : IDiscordCommand
{
    string UserCommandId { get; }
    public Task ExecuteUserCommandAsync(SocketUserCommand command);
    public UserCommandBuilder BuildUserCommand();
}