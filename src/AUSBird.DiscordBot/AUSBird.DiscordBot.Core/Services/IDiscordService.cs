using Discord.Rest;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Services;

public interface IDiscordService : IDisposable
{
    int NodeId { get; }
    int ShardsPerNode { get; }
    int TotalShards { get; }
    Task StartupAsync();
    Task ShutdownAsync();
    DiscordShardedClient GetDiscordSocketClient();
    DiscordRestClient GetDiscordRestClient();
}