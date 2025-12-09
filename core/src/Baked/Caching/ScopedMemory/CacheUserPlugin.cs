using Baked.Ui.Configuration;

namespace Baked.Caching.ScopedMemory;

public record CacheUserPlugin()
    : PluginBase(Module: true)
{
    public int ExpirationInMinutes { get; set; } = 60;
}