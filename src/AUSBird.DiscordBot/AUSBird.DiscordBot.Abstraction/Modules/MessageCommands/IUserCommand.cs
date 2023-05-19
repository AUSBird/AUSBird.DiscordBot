using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.MessageCommands;

public interface IMessageCommand : IDiscordCommand
{
    public Task ExecuteMessageCommandAsync(SocketMessageCommand command);
}