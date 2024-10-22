using Baked.Cors;
using Baked.Cors.AllowOrigin;
using Baked.Runtime;

namespace Baked;

public static class AllowOriginCorsExtensions
{
    public static AllowOriginCorsFeature AllowOrigin(this CorsConfigurator _, params Setting<string>[] origins) =>
          new(origins);
}