using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked.CodingStyle.LocatableExtension;

public class LocatorTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
            "Baked.Orm"
        ];

    List<TypeModel> _entityExtensionTypes = [];

    public LocatorTemplate(DomainModel domain)
    {
        foreach (var item in domain.Types.Having<LocatableExtensionAttribute>())
        {
            if (!item.GetMembers().TryGet<LocatableAttribute>(out var _)) { continue; }

            _entityExtensionTypes.Add(item);
            item.Apply(t => References.Add(t.Assembly));
        }
    }

    public List<Assembly> References { get; } = [];

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

        {{ForEach(_entityExtensionTypes, extension => $$"""
        public class {{extension.Name}}Locator(ILocator<{{EntityName(extension)}}> _entityLocator)
            : ILocator<{{extension.CSharpFriendlyFullName}}>
        {
            public {{extension.CSharpFriendlyFullName}} Locate(Baked.Business.Id id, bool throwNotFound) =>
                ({{extension.CSharpFriendlyFullName}})_entityLocator.Locate(id, throwNotFound: throwNotFound);

            public IEnumerable<{{extension.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Baked.Business.Id> ids) =>
                _entityLocator.LocateMany(ids).Select(e => ({{extension.CSharpFriendlyFullName}})e);
        }
        """)}}
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
            {{ForEach(_entityExtensionTypes, extension => $$"""
                services.AddSingleton<ILocator<{{extension.CSharpFriendlyFullName}}>, {{extension.Name}}Locator>();
            """)}}
            }
        }
    """;

    string EntityName(TypeModel extension) =>
        extension.GetMetadata().Get<LocatableExtensionAttribute>().LocatableType.GetCSharpFriendlyFullName();
}