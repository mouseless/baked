using Do.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Authorization.ClaimBased;

public class ClaimBasedAuthorizationFeature(KeyValuePair<string, Action<AuthorizationPolicyBuilder>>[] _policies)
    : IFeature<AuthorizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddAuthorization(options =>
            {
                foreach (var item in _policies)
                {
                    options.AddPolicy(item.Key, item.Value);
                }
            });

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultHandler>();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseAuthorization(), order: 1);
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new AddAuthorizeAttributeToActionConvention());
        });
    }
}