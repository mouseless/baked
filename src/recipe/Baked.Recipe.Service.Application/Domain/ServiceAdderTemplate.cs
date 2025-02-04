using Baked.CodeGeneration;
using Baked.Domain.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Domain;
internal class ServiceAdderTemplate(List<ServiceModel> _descriptors) : CodeTemplateBase
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
                {{Service(descriptor.ServiceType, descriptor.ServiceType, descriptor.Lifetime)}}
                {{If(descriptor.UseFactory, () => Factory(descriptor.ServiceType, descriptor.ServiceType))}}
                {{ForEach(descriptor.Interfaces, @interface => $$"""
                    {{If(descriptor.Forward,
                        () => Forward(@interface.Model, descriptor.ServiceType, descriptor.Lifetime),
                        () => Service(@interface.Model, descriptor.ServiceType, descriptor.Lifetime))}}
                    {{If(descriptor.UseFactory, () => Factory(@interface.Model, descriptor.ServiceType))}}
                """)}}
            """)}}
            }
        }
    """;

    string Service(TypeModel service, TypeModel implementation, ServiceLifetime lifetime) =>
        $$"""services.Add{{lifetime}}<{{service.CSharpFriendlyFullName}}, {{implementation.CSharpFriendlyFullName}}> ();""";

    string Factory(TypeModel service, TypeModel implementation) =>
        $$"""services.AddSingleton<Func<{{service.CSharpFriendlyFullName}}>>(sp => () => sp.UsingCurrentScope().GetRequiredService<{{implementation.CSharpFriendlyFullName}}>());""";

    string Forward(TypeModel service, TypeModel implementation, ServiceLifetime lifetime) =>
        $$"""services.Add{{lifetime}}<{{service.CSharpFriendlyFullName}}>(sp => ({{service.CSharpFriendlyFullName}})sp.UsingCurrentScope().GetRequiredService<{{implementation.CSharpFriendlyFullName}}>());""";
}