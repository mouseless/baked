using Baked.CodeGeneration;

namespace Baked.CodingStyle.RichTransient;

public class LocatorAdderTemplate(List<(Type Service, Type Implementation)> locators) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [Locators()];

    string Locators() => $$"""
    public class LocatorAdder : IServiceAdder
    {
        public void AddServices(IServiceCollection services)
        {
            {{ForEach(locators, (item) => $$"""
            services.AddSingleton({{item.Service}}, {{item.Implementation}}, forward: true);
            """)}}
        }
    }
    """;
}