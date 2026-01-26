using Baked.CodeGeneration;

namespace Baked.CodingStyle.RichTransient;

public class LocatorAdderTemplate(List<(string Service, string Implementation)> locators) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [Locators()];

    string Locators() => $$"""
    using Baked;
    using Baked.Domain;
    using Baked.Runtime;
    using Microsoft.Extensions.DependencyInjection;

    namespace RichTransient;

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