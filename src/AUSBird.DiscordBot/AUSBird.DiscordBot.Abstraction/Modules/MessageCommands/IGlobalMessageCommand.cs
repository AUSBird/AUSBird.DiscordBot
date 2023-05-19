using Discord;

namespace AUSBird.DiscordBot.Abstraction.Modules.MessageCommands;

public interface IGlobalMessageCommand : IMessageCommand, IDiscordGlobalCommand
{
    public MessageCommandBuilder BuildGlobalMessageCommand();
}