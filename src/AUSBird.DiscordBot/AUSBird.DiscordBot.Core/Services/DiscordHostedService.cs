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
        _discordService.GetDiscordSocketClient().ShardReady += OnReady;
        // Messages
        _discordService.GetDiscordSocketClient().MessageReceived += OnMessageReceived;
        _discordService.GetDiscordSocketClient().MessageUpdated += OnMessageUpdated;
        _discordService.GetDiscordSocketClient().MessageDeleted += OnMessageDeleted;
        // Guilds
        _discordService.GetDiscordSocketClient().JoinedGuild += OnJoinedGuild;
        _discordService.GetDiscordSocketClient().LeftGuild += OnLeftGuild;
        _discordService.GetDiscordSocketClient().GuildUpdated += OnGuildUpdated;
        _discordService.GetDiscordSocketClient().GuildAvailable += OnGuildAvailable;
        // Roles
        _discordService.GetDiscordSocketClient().RoleCreated += OnRoleCreated;
        _discordService.GetDiscordSocketClient().RoleDeleted += OnRoleDeleted;
        _discordService.GetDiscordSocketClient().RoleUpdated += OnRoleUpdated;
        // User
        _discordService.GetDiscordSocketClient().UserBanned += OnUserBanned;
        _discordService.GetDiscordSocketClient().UserJoined += OnUserJoined;
        _discordService.GetDiscordSocketClient().UserLeft += OnUserLeft;
        _discordService.GetDiscordSocketClient().UserUnbanned += OnUserUnbanned;
        _discordService.GetDiscordSocketClient().UserUpdated += OnUserUpdated;
        _discordService.GetDiscordSocketClient().GuildMemberUpdated += OnGuildMemberUpdated;
        // Interactions
        _discordService.GetDiscordSocketClient().SlashCommandExecuted += OnSlashCommandExecuted;
        _discordService.GetDiscordSocketClient().UserCommandExecuted += OnUserCommandExecuted;
        _discordService.GetDiscordSocketClient().MessageCommandExecuted += OnMessageCommandExecuted;
        _discordService.GetDiscordSocketClient().AutocompleteExecuted += OnAutocompleteExecuted;
        _discordService.GetDiscordSocketClient().ModalSubmitted += OnModalSubmitted;
        _discordService.GetDiscordSocketClient().ButtonExecuted += OnButtonExecuted;
        // Reactions
        _discordService.GetDiscordSocketClient().ReactionAdded += OnReactionAdded;
        _discordService.GetDiscordSocketClient().ReactionRemoved += OnReactionRemoved;
        _discordService.GetDiscordSocketClient().ReactionsCleared += OnReactionsCleared;
        _discordService.GetDiscordSocketClient().ReactionsRemovedForEmote += OnReactionsRemovedForEmote;
    }

    // Events

    #region Discord System Events

    private async Task OnReady(DiscordSocketClient readyEvent)
    {
        var discordRest = _discordService.GetDiscordSocketClient().Rest;

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