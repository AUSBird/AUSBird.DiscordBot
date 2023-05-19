using AUSBird.DiscordBot.Abstraction.Modules;
using AUSBird.DiscordBot.Abstraction.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AUSBird.DiscordBot.Services;

public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public EventService(ILogger<EventService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public IEnumerable<IDiscordEvent> ListEventHandlers()
    {
        return _serviceProvider.GetServices<IDiscordEvent>();
    }

    public IEnumerable<TEvent> ListEventHandlers<TEvent>() where TEvent : IDiscordEvent
    {
        return _serviceProvider.GetServices<TEvent>();
    }
}