using Baked.CodeGeneration;

namespace Baked.Domain;

//TODO requires review, temporarily placed here for access from features
public class GeneratedServiceAdderTemplate(List<GeneratedServiceDescriptor> generatedServices) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [Generated()];

    string Generated() => $$"""
    using Baked;
    using Baked.Domain;
    using Baked.Runtime;
    using Microsoft.Extensions.DependencyInjection;

    namespace Domain;

    public class GeneratedServiceAdder : IServiceAdder
    {
        public void AddServices(IServiceCollection services)
        {
            {{ForEach(generatedServices, (item) => $$"""
            services.AddSingleton<{{item.Implementation}}>();
            services.AddSingleton<{{item.Service}}, {{item.Implementation}}>(forward: true);
            """)}}
        }
    }
    """;
}