using Baked.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Baked.RateLimiter.Concurrency;

public class ConcurrencyRateLimiterFeature(ConcurrencyLimiterOptions _options) : IFeature<RateLimiterConfigurator>
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
                    options.PermitLimit = _options.PermitLimit;
                    options.QueueProcessingOrder = _options.QueueProcessingOrder;
                    options.QueueLimit = _options.QueueLimit;
                }));
        });

        configurator.ConfigureThreadOptions(options =>
        {
            options.MinThreadCount = _options.PermitLimit * 2;
            options.MaxThreadCount = _options.PermitLimit * 4;
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