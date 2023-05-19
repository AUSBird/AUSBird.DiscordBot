using AUSBird.DiscordBot.Abstraction.Modules;

namespace AUSBird.DiscordBot.Abstraction.Services;

public interface IDiscordEventService
{
    public IEnumerable<IDiscordEvent> ListEventHandlers();
    public IEnumerable<TEvent> ListEventHandlers<TEvent>() where TEvent : IDiscordEvent;
}