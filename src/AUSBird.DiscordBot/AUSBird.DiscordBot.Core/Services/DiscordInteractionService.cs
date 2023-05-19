using AUSBird.DiscordBot.Abstraction.Modules.Interactions;
using AUSBird.DiscordBot.Abstraction.Services;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IDiscordInteraction = AUSBird.DiscordBot.Abstraction.Modules.IDiscordInteraction;

namespace AUSBird.DiscordBot.Services;

public class DiscordInteractionService : IDiscordInteractionService, IDisposable
{
    private readonly ILogger<DiscordInteractionService> _logger;
    private readonly IServiceScope _serviceScope;

    public DiscordInteractionService(ILogger<DiscordInteractionService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _serviceScope = scopeFactory.CreateScope();
    }

    public async Task ModalSubmittedAsync(SocketModal modal)
    {
        var module = GetModules<IDiscordModalSubmit>().FirstOrDefault(x => x.ModalId == modal.Data.CustomId);

        if (module == null)
        {
            _logger.LogWarning("Unable to find modal submit handler service for {ModalModule} ({Id})",
                modal.Data.CustomId, modal.Data.CustomId);

            var errorEmbed = new EmbedBuilder();
            errorEmbed.Color = Color.Red;
            errorEmbed.Title = "Oops, it seems the modal you submitted is can't be handled.";

            await modal.RespondAsync(embed: errorEmbed.Build(), ephemeral: true);
        }
        else
        {
            _logger.LogInformation("Executing modal submit handler service {Service} for modal submit {ModalModule}",
                module.GetType().FullName, modal.Data.CustomId);

            await module.ModalSubmittedAsync(modal);
        }
    }

    public async Task MessageComponentInteractedAsync(SocketMessageComponent component)
    {
        var module = GetModules<IDiscordComponentInteraction>()
            .FirstOrDefault(x => x.ComponentIds.Contains(component.Data.CustomId));

        if (module == null)
        {
            _logger.LogWarning("Unable to find component interaction handler service for {ComponentModule} ({Id})",
                component.Data.CustomId, component.Data.CustomId);

            var errorEmbed = new EmbedBuilder();
            errorEmbed.Color = Color.Red;
            errorEmbed.Title = "Oops, it seems the component you interacted with can't be handled.";

            await component.RespondAsync(embed: errorEmbed.Build(), ephemeral: true);
        }
        else
        {
            _logger.LogInformation(
                "Executing component interaction handler service {Service} for component interaction {ComponentModule}",
                module.GetType().FullName, component.Data.CustomId);

            await module.ExecuteInteractionAsync(component);
        }
    }

    public void Dispose()
    {
        _serviceScope.Dispose();
    }

    #region Helpers

    private IEnumerable<TInteraction> GetModules<TInteraction>() where TInteraction : IDiscordInteraction
    {
        return _serviceScope.ServiceProvider.GetServices<TInteraction>();
    }

    #endregion
}