using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AUSBird.DiscordBot.Services;

public class DiscordEventService : IDiscordEventService, IDisposable
{
    private readonly ILogger<DiscordEventService> _logger;
    private readonly IServiceScope _serviceScope;

    public DiscordEventService(ILogger<DiscordEventService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _serviceScope = scopeFactory.CreateScope();
    }

    public IEnumerable<IDiscordEvent> ListEventHandlers()
    {
        return _serviceScope.ServiceProvider.GetServices<IDiscordEvent>();
    }

    public IEnumerable<TEvent> ListEventHandlers<TEvent>() where TEvent : IDiscordEvent
    {
        return _serviceScope.ServiceProvider.GetServices<TEvent>();
    }

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}