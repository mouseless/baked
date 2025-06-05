using Baked.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Baked.RateLimiter.Concurrency;

public class ConcurrencyRateLimiterFeature(
    int? permitLimit = default,
    int? queueLimit = default
) : IFeature<RateLimiterConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.Add(new AddRequireConcurrencyLimiterConvention());
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddRateLimiter(options =>
                options.AddConcurrencyLimiter(policyName: "Concurrency", options =>
                {
                    options.PermitLimit = permitLimit ?? (configurator.IsDevelopment() ? 5 : 20);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = queueLimit ?? (configurator.IsDevelopment() ? 100 : 1000);
                }));
        });

        configurator.ConfigureThreadOptions(options =>
        {
            var limit = permitLimit ?? (configurator.IsDevelopment() ? 5 : 20);
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