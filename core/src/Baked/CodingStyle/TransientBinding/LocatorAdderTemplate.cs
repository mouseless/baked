using Baked.CodeGeneration;

namespace Baked.CodingStyle.TransientBinding;

// TODO remove after refactoring `ServiceAdderTemplate` to support generated types
public class LocatorAdderTemplate(List<LocatorDescriptor> locators) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [Locators()];

    string Locators() => $$"""
    using Baked;
    using Baked.Domain;
    using Baked.Runtime;
    using Microsoft.Extensions.DependencyInjection;

    namespace TransientBinding;

    public class LocatorAdder : IServiceAdder
    {
        public void AddServices(IServiceCollection services)
        {
            {{ForEach(locators, (item) => $$"""
            services.AddSingleton<{{item.Implementation}}>();
            services.AddSingleton<{{item.Service}}, {{item.Implementation}}>(forward: true);
            """)}}
        }
    }
    """;
}
