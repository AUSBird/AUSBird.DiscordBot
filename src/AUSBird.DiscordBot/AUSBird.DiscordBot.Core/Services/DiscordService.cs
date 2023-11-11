using System.Diagnostics.CodeAnalysis;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AUSBird.DiscordBot.Services;

public class DiscordService : IDiscordService
{
    private readonly DiscordShardedClient _discordSocketClient;
    private readonly DiscordRestClient _discordRestClient;
    private readonly ILogger<DiscordShardedClient> _discordShardedLogger;
    private readonly ILogger<DiscordRestClient> _discordRestLogger;
    private readonly ILogger<DiscordService> _logger;
    private readonly DiscordServiceConfig _options;

    public DiscordService(IOptions<DiscordServiceConfig> options, ILoggerFactory loggerFactory)
    {
        _discordShardedLogger = loggerFactory.CreateLogger<DiscordShardedClient>();
        _discordRestLogger = loggerFactory.CreateLogger<DiscordRestClient>();

        _logger = loggerFactory.CreateLogger<DiscordService>();
        _options = options.Value;

        _discordSocketClient = new DiscordShardedClient(_options.GetShardIds(), new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.All,
            TotalShards = _options.TotalShards
        });
        _discordRestClient = new DiscordRestClient(new DiscordRestConfig()
        {
            DefaultRetryMode = RetryMode.AlwaysFail
        });

        _discordSocketClient.ShardReady += OnShardReady;
        _discordSocketClient.Log += OnSocketLog;
        _discordRestClient.Log += OnRestLog;
    }

    public void Dispose()
    {
        _discordSocketClient.Dispose();
    }

    private async Task OnShardReady(DiscordSocketClient shard)
    {
        _logger.LogInformation("Shard {ShardId} is now ready", shard.ShardId);
    }

    #region Discord System Events

    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    private Task OnSocketLog(LogMessage log)
    {
        Task.Run(() =>
        {
            switch (log.Severity)
            {
                case LogSeverity.Verbose:
                    _discordShardedLogger.LogTrace(log.Exception, log.Message);
                    break;
                case LogSeverity.Debug:
                    _discordShardedLogger.LogDebug(log.Exception, log.Message);
                    break;
                case LogSeverity.Info:
                    _discordShardedLogger.LogInformation(log.Exception, log.Message);
                    break;
                case LogSeverity.Warning:
                    _discordShardedLogger.LogWarning(log.Exception, log.Message);
                    break;
                case LogSeverity.Error:
                    _discordShardedLogger.LogError(log.Exception, log.Message);
                    break;
                case LogSeverity.Critical:
                    _discordShardedLogger.LogCritical(log.Exception, log.Message);
                    break;
            }
        });
        return Task.CompletedTask;
    }

    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    private Task OnRestLog(LogMessage log)
    {
        Task.Run(() =>
        {
            switch (log.Severity)
            {
                case LogSeverity.Verbose:
                    _discordRestLogger.LogTrace(log.Exception, log.Message);
                    break;
                case LogSeverity.Debug:
                    _discordRestLogger.LogDebug(log.Exception, log.Message);
                    break;
                case LogSeverity.Info:
                    _discordRestLogger.LogInformation(log.Exception, log.Message);
                    break;
                case LogSeverity.Warning:
                    _discordRestLogger.LogWarning(log.Exception, log.Message);
                    break;
                case LogSeverity.Error:
                    _discordRestLogger.LogError(log.Exception, log.Message);
                    break;
                case LogSeverity.Critical:
                    _discordRestLogger.LogCritical(log.Exception, log.Message);
                    break;
            }
        });
        return Task.CompletedTask;
    }

    #endregion

    #region Discord System

    public async Task StartupAsync()
    {
        await _discordSocketClient.LoginAsync(TokenType.Bot, _options.BotToken);
        await _discordSocketClient.StartAsync();
        await _discordRestClient.LoginAsync(TokenType.Bot, _options.BotToken);
    }

    public async Task ShutdownAsync()
    {
        await _discordSocketClient.StopAsync();
        await _discordRestClient.LogoutAsync();
    }

    public DiscordShardedClient GetDiscordSocketClient()
    {
        return _discordSocketClient;
    }

    public DiscordRestClient GetDiscordRestClient()
    {
        return _discordRestClient;
    }

    public int NodeId => _options.NodeId;
    public int ShardsPerNode => _options.ShardsPerNode;
    public int TotalShards => _options.ShardsPerNode;

    #endregion
}