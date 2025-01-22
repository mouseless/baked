using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Lifetime.Transient;

public class TransientServiceAdderTemplate(DomainModel _domain) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [ServiceAdder()];

    string ServiceAdder() => $$"""
        namespace TransientFeature;

        public class TransientServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_domain.Types.Having<TransientAttribute>(), scoped => $$"""
                services.AddTransientWithFactory<{{scoped.CSharpFriendlyFullName}}>();
                {{ForEach(scoped.GetInheritance().Interfaces.Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>()), @interface => $$"""
                    services.AddTransientWithFactory<{{@interface.Model.CSharpFriendlyFullName}}, {{scoped.CSharpFriendlyFullName}}>();
                """)}}
            """)}}
            }
        }
    """;
}