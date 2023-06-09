using AUSBird.DiscordBot.Services;
using Discord;
using Discord.WebSocket;

namespace AUSBird.DiscordBot.Extensions;

public static class ShardExtensions
{
    public static DiscordSocketClient GetChannelShard(this IDiscordService service, ISocketMessageChannel channel)
    {
        return service.GetDiscordClient().GetChannelShard(channel);
    }

    public static DiscordSocketClient GetChannelShard(this DiscordShardedClient client, ISocketMessageChannel channel)
    {
        if (channel is IGuildChannel guildChannel) return client.GetShardFor(guildChannel.Guild);

        return client.GetShard(0);
    }
}