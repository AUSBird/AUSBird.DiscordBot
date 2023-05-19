using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Abstraction.Modules.MessageCommands;

public interface IMessageCommand : IDiscordCommand
{
    string MessageCommandId { get; }
    public Task ExecuteMessageCommandAsync(SocketMessageCommand command);
    public MessageCommandBuilder BuildMessageCommand();
}