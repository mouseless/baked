using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Lifetime.Singleton;

public class SingletonServiceAdderTemplate(DomainModel _domain) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [ServiceAdder()];

    string ServiceAdder() => $$"""
        namespace SingletonFeature;

        public class SingletonServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_domain.Types.Having<SingletonAttribute>(), scoped => $$"""
                services.AddSingleton<{{scoped.CSharpFriendlyFullName}}>();
                {{ForEach(scoped.GetInheritance().Interfaces.Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>()), @interface => $$"""
                    services.AddSingleton<{{@interface.Model.CSharpFriendlyFullName}}, {{scoped.CSharpFriendlyFullName}}>(forward: true);
                """)}}
            """)}}
            }
        }
    """;
}