using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Baked.RateLimiter.Concurrency;

public class ConcurrencyRateLimiterFeature(
    Setting<int>? _permitLimit = default,
    Setting<int>? _queueLimit = default
) : IFeature<RateLimiterConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodAttributeConfiguration<ActionModelAttribute>(action =>
                action.AdditionalAttributes.Add("""EnableRateLimiting("Concurrency")""")
            );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddRateLimiter(options =>
                options.AddConcurrencyLimiter(policyName: "Concurrency", options =>
                {
                    options.PermitLimit = _permitLimit?.GetValue() ?? (configurator.IsDevelopment() ? 5 : 20);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = _queueLimit?.GetValue() ?? (configurator.IsDevelopment() ? 100 : 1000);
                })
            );
        });

        configurator.ConfigureThreadOptions(options =>
        {
            var limit = _permitLimit?.GetValue() ?? (configurator.IsDevelopment() ? 5 : 20);
            options.MinThreadCount = limit * 2;
            options.MaxThreadCount = limit * 4;
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseRateLimiter(), order: -30);
        });

        configurator.ConfigureApiModel(api =>
        {
            api.Usings.Add("Microsoft.AspNetCore.RateLimiting");
        });
    }
}