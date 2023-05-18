using System.Diagnostics.CodeAnalysis;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AUSBird.DiscordBot.Services;

public class DiscordService : IDiscordService
{
    private readonly DiscordServiceConfig _options;
    private readonly ILogger<DiscordService> _logger;
    private readonly ILogger<DiscordShardedClient> _discordLogger;
    private readonly DiscordShardedClient _discordClient;

    public DiscordService(IOptions<DiscordServiceConfig> options, ILoggerFactory loggerFactory)
    {
        _discordLogger = loggerFactory.CreateLogger<DiscordShardedClient>();
        _logger = loggerFactory.CreateLogger<DiscordService>();
        _options = options.Value;

        _discordClient = new DiscordShardedClient(_options.GetShardIds(), new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
            TotalShards = _options.TotalShards
        });
        _discordClient.ShardReady += OnShardReady;
        _discordClient.Log += OnLog;
    }

    private async Task OnShardReady(DiscordSocketClient shard)
    {
        _logger.LogInformation("Shard {ShardId} is now ready", shard.ShardId);
    }

    #region Discord System Events

    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    private Task OnLog(LogMessage log)
    {
        Task.Run(() =>
        {
            switch (log.Severity)
            {
                case LogSeverity.Verbose:
                    _discordLogger.LogTrace(log.Exception, log.Message);
                    break;
                case LogSeverity.Debug:
                    _discordLogger.LogDebug(log.Exception, log.Message);
                    break;
                case LogSeverity.Info:
                    _discordLogger.LogInformation(log.Exception, log.Message);
                    break;
                case LogSeverity.Warning:
                    _discordLogger.LogWarning(log.Exception, log.Message);
                    break;
                case LogSeverity.Error:
                    _discordLogger.LogError(log.Exception, log.Message);
                    break;
                case LogSeverity.Critical:
                    _discordLogger.LogCritical(log.Exception, log.Message);
                    break;
            }
        });
        return Task.CompletedTask;
    }

    #endregion

    #region Discord System

    public async Task StartupAsync()
    {
        await _discordClient.LoginAsync(TokenType.Bot, _options.BotToken);
        await _discordClient.StartAsync();
    }

    public async Task ShutdownAsync()
    {
        await _discordClient.StopAsync();
    }

    public DiscordShardedClient GetDiscordClient() => _discordClient;
    public int NodeId => _options.NodeId;
    public int ShardsPerNode => _options.ShardsPerNode;
    public int TotalShards => _options.ShardsPerNode;

    #endregion
    
    public void Dispose()
    {
        _discordClient.Dispose();
    }
}