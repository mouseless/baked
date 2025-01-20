using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Lifetime.Scoped;

public class ScopedServiceAdderTemplate(DomainModel _domain) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [ServiceAdder()];

    string ServiceAdder() => $$"""
        namespace ScopedLifetimeFeature;

        public class ScopedServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_domain.Types.Having<ScopedAttribute>(), scoped => $$"""
                services.AddScopedWithFactory<{{scoped.CSharpFriendlyFullName}}>();
                {{ForEach(scoped.GetInheritance().Interfaces.Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>()), @interface => $$"""
                    services.AddScopedWithFactory<{{@interface.Model.CSharpFriendlyFullName}}, {{scoped.CSharpFriendlyFullName}}>();
                """)}}
            """)}}
            }
        }
    """;
}