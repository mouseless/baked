using Baked.Architecture;
using Baked.CodingStyle.SuffixBasedClient;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked;

public static class SuffixBasedClientCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> SuffixBasedClient(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<Client>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c => c.Type.IsInterface && c.Type.Name.EndsWith("Client"),
                attribute: () => new Client(),
                order: Order.At.Infra
            );

            conventions.RemoveTypeAttribute<ApiController>(
                when: c => c.Type.Name.EndsWith("Client"),
                order: Order.At.Max
            );
        });

        configurator.Buildtime.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(SuffixBasedClientCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<CodingStyleFeature.Configurator>()
                        .AddCodes(new ClientTemplate(domain)),
                    usings: [.. ClientTemplate.GlobalUsings]
                );
            });
        });

        configurator.Testing.ConfigureTestConfiguration(tests =>
        {
            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var clients = generatedContext
                    .Assemblies[nameof(SuffixBasedClientCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IEnumerable<Type>>();

                foreach (var client in clients)
                {
                    tests.Mocks.Add(client, singleton: true);
                }
            });
        });
    });
}