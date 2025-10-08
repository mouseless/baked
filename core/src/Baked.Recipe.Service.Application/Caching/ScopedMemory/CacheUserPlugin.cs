using Baked.Ui;

namespace Baked.Caching.ScopedMemory;

public record CacheUserPlugin : PluginBase
{
    public int ExpirationInMinutes { get; set; } = 60;
}