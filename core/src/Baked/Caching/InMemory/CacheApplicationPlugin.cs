using Baked.Ui.Configuration;

namespace Baked.Caching.InMemory;

public record CacheApplicationPlugin()
    : PluginBase(Module: true)
{
    public int ExpirationInMinutes { get; set; } = 60;
}