using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUSBird.DiscordBot.Abstraction.Plugins;

public interface IDiscordPlugin
{
    void OnSetup(IServiceCollection container, IConfiguration configuration);
    void OnStart(CancellationToken cancellationToken);
    void OnShutdown(CancellationToken cancellationToken);
}