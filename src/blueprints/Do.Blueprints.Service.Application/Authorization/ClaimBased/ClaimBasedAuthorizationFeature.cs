using Do.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Authorization.ClaimBased;

public class ClaimBasedAuthorizationFeature : IFeature<AuthorizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("AdminToken"));
            });
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseAuthorization());
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new AddAuthorizeAttributeToActionConvention());
        });
    }
}