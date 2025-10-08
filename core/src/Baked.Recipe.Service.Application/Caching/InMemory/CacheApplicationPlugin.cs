using Baked.Ui;

namespace Baked.Caching.InMemory;

public record CacheApplicationPlugin : PluginBase
{
    public int ExpirationInMinutes { get; set; } = 60;
}