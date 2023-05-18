using Discord.WebSocket;

namespace AUSBird.DiscordBot.Services;

public interface IDiscordService : IDisposable
{
    Task StartupAsync();
    Task ShutdownAsync();
    DiscordShardedClient GetDiscordClient();
    int NodeId { get; }
    int ShardsPerNode { get; }
    int TotalShards { get; }
}