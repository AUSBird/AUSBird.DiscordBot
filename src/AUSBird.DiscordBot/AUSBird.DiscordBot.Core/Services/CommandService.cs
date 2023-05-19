using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Services;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AUSBird.DiscordBot.Services;

public class CommandService : ICommandService
{
    private readonly ILogger<CommandService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ICommandModuleMapper _commandModuleMapper;

    public CommandService(ILogger<CommandService> logger, IServiceProvider serviceProvider,
        ICommandModuleMapper commandModuleMapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _commandModuleMapper = commandModuleMapper;
    }

    public async Task ExecuteSlashCommandAsync(SocketSlashCommand socketCommand)
    {
        var command = _commandModuleMapper.GetSlashCommandModule(socketCommand.CommandId);
        if (command == null)
        {
            _logger.LogWarning("Unable to find slash command service for {Command}", socketCommand.Data.Name);

            var errorEmbed = new EmbedBuilder();
            errorEmbed.Color = Color.Red;
            errorEmbed.Title = "Oops, it seems the command you ran is missing.";

            await socketCommand.RespondAsync(embed: errorEmbed.Build(), ephemeral: true);
        }
        else
        {
            _logger.LogInformation("Executing slash command service {Service} for slash command {Command}",
                command.GetType().FullName, socketCommand.Data.Name);

            await command.ExecuteSlashCommandAsync(socketCommand);
        }
    }

    public async Task ExecuteUserCommandAsync(SocketUserCommand socketCommand)
    {
        var command = _commandModuleMapper.GetUserCommandModule(socketCommand.CommandId);
        if (command == null)
        {
            _logger.LogWarning("Unable to find user command service for {Command}", socketCommand.Data.Name);

            var errorEmbed = new EmbedBuilder();
            errorEmbed.Color = Color.Red;
            errorEmbed.Title = "Oops, it seems the command you ran is missing.";

            await socketCommand.RespondAsync(embed: errorEmbed.Build(), ephemeral: true);
        }
        else
        {
            _logger.LogInformation("Executing user command service {service} for user command {Command}",
                command.GetType().FullName, socketCommand.Data.Name);

            await command.ExecuteUserCommandAsync(socketCommand);
        }
    }

    public async Task ExecuteMessageCommandAsync(SocketMessageCommand socketCommand)
    {
        var command = _commandModuleMapper.GetMessageCommandModule(socketCommand.CommandId);
        if (command == null)
        {
            _logger.LogWarning("Unable to find message command service for {Command}", socketCommand.Data.Name);

            var errorEmbed = new EmbedBuilder();
            errorEmbed.Color = Color.Red;
            errorEmbed.Title = "Oops, it seems the command you ran is missing.";

            await socketCommand.RespondAsync(embed: errorEmbed.Build(), ephemeral: true);
        }
        else
        {
            _logger.LogInformation("Executing message command service {service} for message command {Command}",
                command.GetType().FullName, socketCommand.Data.Name);

            await command.ExecuteMessageCommandAsync(socketCommand);
        }
    }

    public async Task ExecuteAutocompleteAsync(SocketAutocompleteInteraction autocomplete)
    {
        var command = _commandModuleMapper.GetAutocompleteModule(autocomplete.Data.CommandId);
        if (command == null)
        {
            _logger.LogWarning("Unable to find autocomplete service for {Command}", autocomplete.Data.CommandName);

            await autocomplete.RespondAsync();
        }
        else
        {
            _logger.LogInformation("Executing autocomplete for command service {service} for message command {Command}",
                command.GetType().FullName, autocomplete.Data.CommandName);

            await command.ExecuteAutocompleteAsync(autocomplete);
        }
    }


    private IEnumerable<TCommand> GetModules<TCommand>() where TCommand : IDiscordCommand
    {
        return _serviceProvider.GetServices<TCommand>();
    }
}