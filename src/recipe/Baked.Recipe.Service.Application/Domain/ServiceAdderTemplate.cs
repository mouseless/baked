using Baked.CodeGeneration;

namespace Baked.Domain;
internal class ServiceAdderTemplate(List<DomainServiceDescriptor> _descriptors) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [ServiceAdder()];

    string ServiceAdder() => $$"""
        namespace Domain;

        public class ServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_descriptors, descriptor => $$"""
                services.Add{{descriptor.Lifetime}}<{{descriptor.ServiceType.CSharpFriendlyFullName}}>();
                {{If(descriptor.UseFactory, () => $$"""
                    services.AddSingleton<Func<{{descriptor.ServiceType.CSharpFriendlyFullName}}>>(sp => () => sp.UsingCurrentScope().GetRequiredService<{{descriptor.ServiceType.CSharpFriendlyFullName}}>());
                """)}}
                {{ForEach(descriptor.Interfaces, @interface => $$"""
                    {{If(descriptor.Forward,
                    () => $$"""
                        services.Add{{descriptor.Lifetime}}<{{@interface.Model.CSharpFriendlyFullName}}>(sp => ({{@interface.Model.CSharpFriendlyFullName}})sp.UsingCurrentScope().GetRequiredService<{{descriptor.ServiceType.CSharpFriendlyFullName}}>());
                    """,
                    () => $$"""
                        services.Add{{descriptor.Lifetime}}<{{@interface.Model.CSharpFriendlyFullName}}, {{descriptor.ServiceType.CSharpFriendlyFullName}}> ();
                    """)}}
                    {{If(descriptor.UseFactory, () => $$"""
                        services.AddSingleton<Func<{{@interface.Model.CSharpFriendlyFullName}}>>(sp => () => sp.UsingCurrentScope().GetRequiredService<{{@interface.Model.CSharpFriendlyFullName}}>());
                    """)}}
                """)}}
            """)}}
            }
        }
    """;
}