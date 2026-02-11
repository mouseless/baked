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

    List<TypeModel> _locatableExtensionTypes = [];

    public LocatorTemplate(DomainModel domain)
    {
        foreach (var item in domain.Types.Having<LocatableExtensionAttribute>())
        {
            if (!item.GetMembers().TryGet<LocatableAttribute>(out var _)) { continue; }

            _locatableExtensionTypes.Add(item);
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

        namespace LocatableExtensionCodingStyleFeature;

        {{ForEach(_locatableExtensionTypes, extension => $$"""
        public class {{extension.Name}}Locator(ILocator<{{LocatableTypeName(extension)}}> _locator)
            : ILocator<{{extension.CSharpFriendlyFullName}}>
        {
            public {{extension.CSharpFriendlyFullName}} Locate(Baked.Business.Id id, bool throwNotFound) =>
                ({{extension.CSharpFriendlyFullName}})_locator.Locate(id, throwNotFound: throwNotFound);

            public ({{extension.CSharpFriendlyFullName}}, Task) LocateLazily(Id id)
            {
                var (result, initialize) = _locator.LocateLazily(id);

                return (({{extension.CSharpFriendlyFullName}})result, initialize);
            }

            public IEnumerable<{{extension.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Baked.Business.Id> ids) =>
                _locator.LocateMany(ids).Select(e => ({{extension.CSharpFriendlyFullName}})e);
        }
        """)}}
    """;

    string ServiceAdder() => $$"""
        using Baked;
        using Baked.Domain;
        using Baked.Runtime;
        using Microsoft.Extensions.DependencyInjection;

        namespace LocatableExtensionCodingStyleFeature;

        public class ServiceServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_locatableExtensionTypes, extension => $$"""
                services.AddSingleton<ILocator<{{extension.CSharpFriendlyFullName}}>, {{extension.Name}}Locator>();
            """)}}
            }
        }
    """;

    string LocatableTypeName(TypeModel extension) =>
        extension.GetMetadata().Get<LocatableExtensionAttribute>().LocatableType.GetCSharpFriendlyFullName();
}