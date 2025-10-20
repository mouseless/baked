using Baked.Ui.Configuration;

namespace Baked.Caching.ScopedMemory;

public record CacheUserPlugin : PluginBase
{
    public int ExpirationInMinutes { get; set; } = 60;
}