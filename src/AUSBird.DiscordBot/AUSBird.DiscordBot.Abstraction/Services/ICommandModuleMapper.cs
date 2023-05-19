using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Modules.MessageCommands;
using AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;
using AUSBird.DiscordBot.Abstraction.Modules.UserCommands;

namespace AUSBird.DiscordBot.Abstraction.Services;

public interface ICommandModuleMapper
{
    ISlashCommand? GetSlashCommandModule(ulong commandId);
    ISlashCommandAutocomplete? GetAutocompleteModule(ulong commandId);
    IUserCommand? GetUserCommandModule(ulong commandId);
    IMessageCommand? GetMessageCommandModule(ulong commandId);

    void RegisterCommandModule(ulong commandId, ulong? guildId, Type moduleType);
    bool CommandExists(ulong commandId);
}