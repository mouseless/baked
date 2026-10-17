using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Baked.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Baked;

public static class ConcurrencyRateLimiterFeature
{
    public static IFeature<RateLimiterFeature.Configurator> Concurrency(this RateLimiterFeature.Configurator x,
        Setting<int>? permitLimit = default,
        Setting<int>? queueLimit = default
    ) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditMethodAttribute<ApiAction>(
                attribute: action => action.AdditionalAttributes.Add("""EnableRateLimiting("Concurrency")"""),
                order: Order.At.Infra
            );
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddRateLimiter(options =>
                options.AddConcurrencyLimiter(policyName: "Concurrency", options =>
                {
                    options.PermitLimit = permitLimit?.GetValue() ?? (configurator.IsDevelopment ? 5 : 20);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = queueLimit?.GetValue() ?? (configurator.IsDevelopment ? 100 : 1000);
                })
            );
        });

        configurator.Runtime.ConfigureThreadOptions(options =>
        {
            var limit = permitLimit?.GetValue() ?? (configurator.IsDevelopment ? 5 : 20);
            options.MinThreadCount = limit * 2;
            options.MaxThreadCount = limit * 4;
        });

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseRateLimiter(), order: -30);
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.Usings.Add("Microsoft.AspNetCore.RateLimiting");
        });
    });
}