namespace AUSBird.DiscordBot.Services;

public class DiscordServiceConfig
{
    public ulong ApplicationId { get; set; }
    public string? ClientSecret { get; set; }
    public string PublicKey { get; set; }
    public string BotToken { get; set; }
    public int NodeId { get; set; }
    public int ShardsPerNode { get; set; }
    public int TotalShards { get; set; }

    public int[] GetShardIds()
    {
        var startId = NodeId * ShardsPerNode;
        var endId = startId + ShardsPerNode;

        var shardIds = new List<int>();
        for (var id = startId; id < endId && id < TotalShards; id++) shardIds.Add(id);

        return shardIds.ToArray();
    }
}