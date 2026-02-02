using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class LocatorTemplate
    : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
            "Baked.Orm"
        ];

    List<TypeModel> _entityExtensionTypes = [];
    List<(string Service, string Implementation)> _generatedServices = [];

    public LocatorTemplate(DomainModel domain)
    {
        foreach (var item in domain.Types.Having<EntityExtensionAttribute>())
        {
            if (!item.GetMembers().TryGet<LocatableAttribute>(out var _)) { continue; }

            _entityExtensionTypes.Add(item);
            _generatedServices.Add((LocatorTypeName(item), ImplementatonTypeName(item)));
            item.Apply(t => Referencs.Add(t.Assembly));
        }
    }

    public List<Assembly> Referencs { get; } = [];

    protected override IEnumerable<string> Render() =>
        [
            Locator(),
            ServiceAdder()
        ];

    string Locator() => $$"""
    using Baked;
    using Baked.Domain;
    using Baked.Runtime;
    using Microsoft.Extensions.DependencyInjection;

    namespace EntityExtensionViaComposition;

    {{ForEach(_entityExtensionTypes, e =>
    $$"""
    public class {{e.Name}}Locator(ILocator<{{EntityName(e)}}> _entityLocator)
        : ILocator<{{e.CSharpFriendlyFullName}}>
    {
        public {{e.CSharpFriendlyFullName}} Locate(Baked.Business.Id id, bool throwNotFound) =>
            ({{e.CSharpFriendlyFullName}})_entityLocator.Locate(id, throwNotFound: throwNotFound);

        public IEnumerable<{{e.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Baked.Business.Id> ids) =>
            _entityLocator.LocateMany(ids).Select(e => ({{e.CSharpFriendlyFullName}})e);
    }
    """
    )}}
    """;

    string ServiceAdder() => $$"""
    using Baked;
    using Baked.Domain;
    using Baked.Runtime;
    using Microsoft.Extensions.DependencyInjection;

    namespace EntityExtensionViaComposition;

    public class ServiceServiceAdder : IServiceAdder
    {
        public void AddServices(IServiceCollection services)
        {
            {{ForEach(_generatedServices, (item) => $$"""
            services.AddSingleton<{{item.Implementation}}>();
            services.AddSingleton<{{item.Service}}, {{item.Implementation}}>(forward: true);
            """)}}
        }
    }
    """;

    string EntityName(TypeModel extension) => extension.GetMetadata().Get<EntityExtensionAttribute>().EntityType.GetCSharpFriendlyFullName();
    string LocatorTypeName(TypeModel extension) => $$"""ILocator<{{extension.CSharpFriendlyFullName}}>""";
    string ImplementatonTypeName(TypeModel extension) => $$"""EntityExtensionViaComposition.{{extension.Name}}Locator""";
}