using AUSBird.DiscordBot.Interfaces;
using AUSBird.DiscordBot.Interfaces.CommandAutocomplete;
using AUSBird.DiscordBot.Interfaces.MessageCommands;
using AUSBird.DiscordBot.Interfaces.SlashCommands;
using AUSBird.DiscordBot.Interfaces.UserCommands;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AUSBird.DiscordBot.Services;

public class DiscordCommandService : IDiscordCommandService, IDisposable
{
    private readonly ILogger<DiscordCommandService> _logger;
    private readonly IServiceScope _serviceScope;

    public DiscordCommandService(ILogger<DiscordCommandService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _serviceScope = scopeFactory.CreateScope();
    }

    public async Task ExecuteSlashCommandAsync(SocketSlashCommand socketCommand)
    {
        var command = GetSlashCommandModule(socketCommand.Data.Name);
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
        var command = GetUserCommandModule(socketCommand.Data.Name);
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
        var command = GetMessageCommandModule(socketCommand.Data.Name);
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
        var command = GetAutocompleteCommandModule(autocomplete.Data.CommandName);
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

    public IEnumerable<string> ListSlashCommandIds() =>
        GetModules<IGlobalSlashCommand>().Select(x => x.SlashCommandId);

    public IEnumerable<string> ListUserCommandIds() =>
        GetModules<IGlobalUserCommand>().Select(x => x.UserCommandId);

    public IEnumerable<string> ListMessageCommandIds() =>
        GetModules<IGlobalMessageCommand>().Select(x => x.MessageCommandId);

    public async Task<IEnumerable<ApplicationCommandProperties>> BuildCommandsModulesAsync()
    {
        var slashCommands = new List<ApplicationCommandProperties>();

        foreach (var module in GetModules<IDiscordCommand>())
        {
            if (module is ISlashCommand slashCommand)
                slashCommands.Add(slashCommand.BuildSlashCommand().Build());
            if (module is IUserCommand userCommand)
                slashCommands.Add(userCommand.BuildUserCommand().Build());
            if (module is IMessageCommand messageCommand)
                slashCommands.Add(messageCommand.BuildMessageCommand().Build());
        }

        return slashCommands;
    }


    private IEnumerable<TCommand> GetModules<TCommand>() where TCommand : IDiscordCommand =>
        _serviceScope.ServiceProvider.GetServices<TCommand>();

    private ISlashCommand? GetSlashCommandModule(string name) => GetModules<ISlashCommand>()
        .FirstOrDefault(x => x.SlashCommandId == name);

    private IUserCommand? GetUserCommandModule(string name) => GetModules<IUserCommand>()
        .FirstOrDefault(x => x.UserCommandId == name);

    private IMessageCommand? GetMessageCommandModule(string name) => GetModules<IMessageCommand>()
        .FirstOrDefault(x => x.MessageCommandId == name);

    private ICommandAutocomplete? GetAutocompleteCommandModule(string name) =>
        GetModules<ICommandAutocomplete>().FirstOrDefault(x => x.SlashCommandId == name);

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}