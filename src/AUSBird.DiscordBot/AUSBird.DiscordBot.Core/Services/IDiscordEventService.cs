using AUSBird.DiscordBot.Interfaces;

namespace AUSBird.DiscordBot.Services;

public interface IDiscordEventService
{
    public IEnumerable<IDiscordEvent> ListEventHandlers();
    public IEnumerable<TEvent> ListEventHandlers<TEvent>() where TEvent : IDiscordEvent;
}