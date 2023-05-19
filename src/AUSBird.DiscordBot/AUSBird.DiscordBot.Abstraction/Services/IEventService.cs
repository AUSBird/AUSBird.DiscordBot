using AUSBird.DiscordBot.Abstraction.Modules;

namespace AUSBird.DiscordBot.Abstraction.Services;

public interface IEventService
{
    public IEnumerable<IDiscordEvent> ListEventHandlers();
    public IEnumerable<TEvent> ListEventHandlers<TEvent>() where TEvent : IDiscordEvent;
}