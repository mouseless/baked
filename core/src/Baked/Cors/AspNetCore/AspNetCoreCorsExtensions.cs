using Baked.Cors;
using Baked.Cors.AllowOrigin;
using Baked.Runtime;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Baked;

public static class AspNetCoreCorsExtensions
{
    extension(CorsConfigurator configurator)
    {
        /// <note>
        /// Returns 'AspNetCoreCors' feature with a single policy setup with given origins,
        /// any header and any method
        /// </note>
        public AspNetCoreCorsFeature AspNetCore(params IEnumerable<Setting<string>> origins) =>
            configurator.AspNetCore(
                options => options
                    .AddPolicy("allow-origin", policy => policy
                        .WithOrigins([.. origins.Select(o => o.GetValue())])
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    ),
                "allow-origin"
            );

        public AspNetCoreCorsFeature AspNetCore(Action<CorsOptions> optionsBuilder, string defaultPolicyName) =>
            new(optionsBuilder, defaultPolicyName);
    }
}