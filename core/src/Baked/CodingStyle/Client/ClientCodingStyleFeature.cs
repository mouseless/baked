using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.Client;

public class ClientCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ClientAttribute>();

            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.IsInterface && c.Type.Name.EndsWith("Client"),
                attribute: () => new ClientAttribute()
            );
            builder.Conventions.RemoveTypeAttribute<ControllerModelAttribute>(
                when: c => c.Type.Name.EndsWith("Client"),
                order: RestApiLayer.MaxConventionOrder - 10
            );
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(ClientCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<ClientCodingStyleFeature>()
                        .AddCodes(new ClientTemplate(domain)),
                    usings: [.. ClientTemplate.GlobalUsings]
                );
            });
        });

        configurator.Testing.ConfigureTestConfiguration(tests =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                var clients = generatedContext
                    .Assemblies[nameof(ClientCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IEnumerable<Type>>();

                foreach (var client in clients)
                {
                    tests.Mocks.Add(client, singleton: true);
                }
            });
        });
    }
}