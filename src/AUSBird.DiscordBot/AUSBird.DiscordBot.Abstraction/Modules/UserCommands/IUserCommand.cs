using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.UserCommands;

public interface IUserCommand : IDiscordCommand
{
    public Task ExecuteUserCommandAsync(SocketUserCommand command);
}