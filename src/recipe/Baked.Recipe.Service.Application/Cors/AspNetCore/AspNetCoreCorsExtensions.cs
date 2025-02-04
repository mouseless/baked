using Baked.Cors;
using Baked.Cors.AllowOrigin;
using Baked.Runtime;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Baked;

public static class AspNetCoreCorsExtensions
{
    /// <note>
    /// Returns 'AspNetCoreCors' feature with a single policy setup with given origins,
    /// any header and any method
    /// </note>
    public static AspNetCoreCorsFeature AspNetCore(this CorsConfigurator configurator, params IEnumerable<Setting<string>> origins) =>
        configurator.AspNetCore(
            options => options
                .AddPolicy("allow-origin", policy => policy
                    .WithOrigins([.. origins.Select(o => o.GetValue())])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                ),
            "allow-origin"
        );

    public static AspNetCoreCorsFeature AspNetCore(this CorsConfigurator _, Action<CorsOptions> optionsBuilder, string defaultPolicyName) =>
        new(optionsBuilder, defaultPolicyName);
}