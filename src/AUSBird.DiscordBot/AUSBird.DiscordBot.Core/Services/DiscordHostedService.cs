using AUSBird.DiscordBot.Abstraction.Extensions;
using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Modules.MessageCommands;
using AUSBird.DiscordBot.Abstraction.Modules.SlashCommands;
using AUSBird.DiscordBot.Abstraction.Modules.UserCommands;
using AUSBird.DiscordBot.Abstraction.Services;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AUSBird.DiscordBot.Services;

public partial class DiscordHostedService : IHostedService, IDisposable
{
    private readonly ICommandService _commandService;
    private readonly IDiscordService _discordService;
    private readonly IEventService _eventService;
    private readonly IInteractionService _interactionService;
    private readonly ICommandModuleMapper _commandModuleMapper;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DiscordHostedService> _logger;

    public DiscordHostedService(ILoggerFactory loggerFactory, ICommandService commandService,
        IEventService eventService, IDiscordService discordService, IInteractionService interactionService,
        ICommandModuleMapper commandModuleMapper, IServiceProvider serviceProvider)
    {
        _logger = loggerFactory.CreateLogger<DiscordHostedService>();
        _commandService = commandService;
        _eventService = eventService;
        _discordService = discordService;
        _interactionService = interactionService;
        _commandModuleMapper = commandModuleMapper;
        _serviceProvider = serviceProvider;

        // System
        _discordService.GetDiscordClient().ShardReady += OnReady;
        // Roles
        _discordService.GetDiscordClient().RoleCreated += OnRoleCreated;
        _discordService.GetDiscordClient().RoleDeleted += OnRoleDeleted;
        _discordService.GetDiscordClient().RoleUpdated += OnRoleUpdated;
        // User
        _discordService.GetDiscordClient().UserBanned += OnUserBanned;
        _discordService.GetDiscordClient().UserJoined += OnUserJoined;
        _discordService.GetDiscordClient().UserLeft += OnUserLeft;
        _discordService.GetDiscordClient().UserUnbanned += OnUserUnbanned;
        _discordService.GetDiscordClient().UserUpdated += OnUserUpdated;
        // Interactions
        _discordService.GetDiscordClient().SlashCommandExecuted += OnSlashCommandExecuted;
        _discordService.GetDiscordClient().UserCommandExecuted += OnUserCommandExecuted;
        _discordService.GetDiscordClient().MessageCommandExecuted += OnMessageCommandExecuted;
        _discordService.GetDiscordClient().AutocompleteExecuted += OnAutocompleteExecuted;
        _discordService.GetDiscordClient().ModalSubmitted += OnModalSubmitted;
        _discordService.GetDiscordClient().ButtonExecuted += OnButtonExecuted;
        // Reactions
        _discordService.GetDiscordClient().ReactionAdded += OnReactionAdded;
        _discordService.GetDiscordClient().ReactionRemoved += OnReactionRemoved;
        _discordService.GetDiscordClient().ReactionsCleared += OnReactionsCleared;
        _discordService.GetDiscordClient().ReactionsRemovedForEmote += OnReactionsRemovedForEmote;
    }

    // Events

    #region Discord System Events

    private async Task OnReady(DiscordSocketClient readyEvent)
    {
        var discordRest = _discordService.GetDiscordClient().Rest;

        foreach (var module in _serviceProvider.GetModules<IDiscordCommand>())
        {
            if (module is IGlobalSlashCommand slashCommand)
            {
                var commandProps = slashCommand.BuildGlobalSlashCommand().Build();
                var discordCommand = await discordRest.CreateGlobalCommand(commandProps);
                _commandModuleMapper.RegisterCommandModule(discordCommand.Id, null, module.GetType());
            }

            if (module is IGlobalUserCommand userCommand)
            {
                var commandProps = userCommand.BuildGlobalUserCommand().Build();
                var discordCommand = await discordRest.CreateGlobalCommand(commandProps);
                _commandModuleMapper.RegisterCommandModule(discordCommand.Id, null, module.GetType());
            }

            if (module is IGlobalMessageCommand messageCommand)
            {
                var commandProps = messageCommand.BuildGlobalMessageCommand().Build();
                var discordCommand = await discordRest.CreateGlobalCommand(commandProps);
                _commandModuleMapper.RegisterCommandModule(discordCommand.Id, null, module.GetType());
            }
        }

        foreach (var discordCommand in await discordRest.GetGlobalApplicationCommands())
        {
            if (!_commandModuleMapper.CommandExists(discordCommand.Id)) await discordCommand.DeleteAsync();
        }
    }

    #endregion

    // Service

    #region Service Functions

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting up the discord shard");
        await _discordService.StartupAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Shutting down the discord shard");
        await _discordService.ShutdownAsync();
    }

    public void Dispose()
    {
        _discordService.Dispose();
    }

    #endregion
}