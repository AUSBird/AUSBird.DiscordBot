using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace AUSBird.DiscordBot.Services;

public partial class DiscordHostedService
{
    private static async Task ReplyWithErrorEmbed(IDiscordInteraction interaction)
    {
        var errorEmbed = new EmbedBuilder();
        errorEmbed.Color = Color.DarkRed;
        errorEmbed.Title = "An error occured while executing your command";

        if (interaction.HasResponded)
            await interaction.DeleteOriginalResponseAsync();
        await interaction.RespondAsync(embed: errorEmbed.Build(), ephemeral: true);
    }

    #region Discord Interaction Events

    private async Task OnSlashCommandExecuted(SocketSlashCommand command)
    {
        try
        {
            await _commandService.ExecuteSlashCommandAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception was encountered while performing slash command");
            if (!command.HasResponded)
                await ReplyWithErrorEmbed(command);
        }
    }

    private async Task OnUserCommandExecuted(SocketUserCommand command)
    {
        try
        {
            await _commandService.ExecuteUserCommandAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception was encountered while performing user command");
            if (!command.HasResponded)
                await ReplyWithErrorEmbed(command);
        }
    }

    private async Task OnMessageCommandExecuted(SocketMessageCommand command)
    {
        try
        {
            await _commandService.ExecuteMessageCommandAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception was encountered while performing message command");
            if (!command.HasResponded)
                await ReplyWithErrorEmbed(command);
        }
    }

    private async Task OnAutocompleteExecuted(SocketAutocompleteInteraction autocomplete)
    {
        try
        {
            await _commandService.ExecuteAutocompleteAsync(autocomplete);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception was encountered while performing autocomplete");
            if (!autocomplete.HasResponded)
                await autocomplete.RespondAsync();
        }
    }

    private async Task OnModalSubmitted(SocketModal modal)
    {
        try
        {
            await _interactionService.ModalSubmittedAsync(modal);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception was encountered while performing modal submit");
            if (!modal.HasResponded)
                await modal.RespondAsync();
        }
    }

    private async Task OnButtonExecuted(SocketMessageComponent button)
    {
        try
        {
            await _interactionService.MessageComponentInteractedAsync(button);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception was encountered while performing button click");
            if (!button.HasResponded)
                await button.RespondAsync();
        }
    }

    #endregion
}