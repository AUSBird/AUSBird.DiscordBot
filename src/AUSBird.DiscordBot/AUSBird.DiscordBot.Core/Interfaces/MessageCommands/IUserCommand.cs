using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Interfaces.MessageCommands;

public interface IMessageCommand : IDiscordCommand
{
    string MessageCommandId { get; }
    public Task ExecuteMessageCommandAsync(SocketMessageCommand command);
    public MessageCommandBuilder BuildMessageCommand();
}