using Baked.Ui.Configuration;

namespace Baked.Caching.InMemory;

public record CacheApplicationPlugin : ModulePluginBase
{
    public int ExpirationInMinutes { get; set; } = 60;
}