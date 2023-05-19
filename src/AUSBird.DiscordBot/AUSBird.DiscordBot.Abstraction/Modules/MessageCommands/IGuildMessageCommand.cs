using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.MessageCommands;

public interface IGuildMessageCommand : IMessageCommand, IDiscordGuildCommand
{
    public MessageCommandBuilder BuildGuildMessageCommand(string name);
}