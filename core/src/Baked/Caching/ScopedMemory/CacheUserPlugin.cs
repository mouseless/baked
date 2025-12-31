using Baked.Ui.Configuration;

namespace Baked.Caching.ScopedMemory;

public record CacheUserPlugin : ModulePluginBase
{
    public int ExpirationInMinutes { get; set; } = 60;
}