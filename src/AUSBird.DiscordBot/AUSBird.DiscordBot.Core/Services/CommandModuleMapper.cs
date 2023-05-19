using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Modules.MessageCommands;
using AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;
using AUSBird.DiscordBot.Abstraction.Modules.UserCommands;
using AUSBird.DiscordBot.Abstraction.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AUSBird.DiscordBot.Services;

public class CommandModuleMapper : ICommandModuleMapper
{
    private readonly IServiceProvider _serviceProvider;
    private readonly List<CommandModuleMapperEntry> _modules;

    public CommandModuleMapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _modules = new List<CommandModuleMapperEntry>();
    }

    public ISlashCommand? GetSlashCommandModule(ulong commandId) => GetModule<ISlashCommand>(commandId);

    public ISlashCommandAutocomplete? GetAutocompleteModule(ulong commandId) =>
        GetModule<ISlashCommandAutocomplete>(commandId);

    public IUserCommand? GetUserCommandModule(ulong commandId) => GetModule<IUserCommand>(commandId);

    public IMessageCommand? GetMessageCommandModule(ulong commandId) => GetModule<IMessageCommand>(commandId);

    public void RegisterCommandModule(ulong commandId, ulong? guildId, Type moduleType)
    {
        _modules.Add(new CommandModuleMapperEntry(commandId, guildId, moduleType));
    }

    public bool CommandExists(ulong commandId)
    {
        return _modules.Any(x => x.CommandId == commandId);
    }

    private TModule? GetModule<TModule>(ulong commandId) where TModule : class, IDiscordModule
    {
        var moduleType = _modules.FirstOrDefault(x => x.CommandId == commandId)?.CommandModuleType;

        if (moduleType == null)
            return null;

        return (TModule)_serviceProvider.GetRequiredService(moduleType);
    }

    private class CommandModuleMapperEntry
    {
        public CommandModuleMapperEntry(ulong commandId, ulong? guildId, Type commandModuleType)
        {
            CommandId = commandId;
            GuildId = guildId;
            CommandModuleType = commandModuleType;
        }

        public ulong? GuildId { get; init; }
        public ulong CommandId { get; init; }
        public Type CommandModuleType { get; init; }
    }
}