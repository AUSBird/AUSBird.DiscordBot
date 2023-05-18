using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AUSBird.DiscordBot.Services;

public partial class DiscordHostedService : IHostedService, IDisposable
{
    private readonly IDiscordCommandService _commandService;
    private readonly IDiscordService _discordService;
    private readonly IDiscordEventService _eventService;
    private readonly IDiscordInteractionService _interactionService;
    private readonly ILogger<DiscordHostedService> _logger;

    public DiscordHostedService(ILoggerFactory loggerFactory, IDiscordCommandService commandService,
        IDiscordEventService eventService, IDiscordService discordService,
        IDiscordInteractionService interactionService)
    {
        _logger = loggerFactory.CreateLogger<DiscordHostedService>();
        _commandService = commandService;
        _eventService = eventService;
        _discordService = discordService;
        _interactionService = interactionService;

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
        await _discordService.GetDiscordClient().SetStatusAsync(UserStatus.DoNotDisturb);

        var commands = await _commandService.BuildCommandsModulesAsync();
        await _discordService.GetDiscordClient().Rest.BulkOverwriteGlobalCommands(commands.ToArray());

        await _discordService.GetDiscordClient().SetStatusAsync(UserStatus.Online);
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