using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.CodingStyle.LocatableExtension;

public class LocatorTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
            "Baked.Orm"
        ];

    readonly DomainModel _domain;
    readonly List<TypeModelMembers> _locatableExtensions = [];

    public LocatorTemplate(DomainModel domain)
    {
        _domain = domain;
        foreach (var item in _domain.Types.Having<LocatableExtensionAttribute>())
        {
            if (!item.TryGetMembers(out var members)) { continue; }
            if (!members.TryGet<LocatableAttribute>(out var _)) { continue; }

            _locatableExtensions.Add(members);
        }

        AddReferences(_locatableExtensions);
    }

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

        {{ForEach(_locatableExtensions, extension => $$"""
        public class {{extension.Name}}Locator(
            I{{If(IsAsync(extension), () => "Async")}}Locator<{{LocatableType(extension).CSharpFriendlyFullName}}> _locator
        ) : I{{If(IsAsync(extension), () => "Async")}}Locator<{{extension.CSharpFriendlyFullName}}>
        {
            {{If(IsAsync(extension), () => $$"""
            public async Task<{{extension.CSharpFriendlyFullName}}> LocateAsync(Id id, bool throwNotFound) =>
                ({{extension.CSharpFriendlyFullName}})await _locator.LocateAsync(id, throwNotFound: throwNotFound);

            public async Task<IEnumerable<{{extension.CSharpFriendlyFullName}}>> LocateManyAsync(IEnumerable<Id> ids) =>
                (await _locator.LocateManyAsync(ids)).Select(e => ({{extension.CSharpFriendlyFullName}})e);

            public {{extension.CSharpFriendlyFullName}} Locate(Id id, bool throwNotFound) =>
                ({{extension.CSharpFriendlyFullName}})_locator.Locate(id, throwNotFound: throwNotFound);

            public LazyLocatable<{{extension.CSharpFriendlyFullName}}> LocateLazily(Id id)
            {
                var result = _locator.LocateLazily(id);

                return new(({{extension.CSharpFriendlyFullName}})result.Value, result.Initialize);
            }

            public IEnumerable<{{extension.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Id> ids) =>
                _locator.LocateMany(ids).Select(e => ({{extension.CSharpFriendlyFullName}})e);
            """,
            @else: () => $$"""
            public {{extension.CSharpFriendlyFullName}} Locate(Id id, bool throwNotFound) =>
                ({{extension.CSharpFriendlyFullName}})_locator.Locate(id, throwNotFound: throwNotFound);

            public LazyLocatable<{{extension.CSharpFriendlyFullName}}> LocateLazily(Id id)
            {
                var result = _locator.LocateLazily(id);

                return new(({{extension.CSharpFriendlyFullName}})result.Value, result.Initialize);
            }

            public IEnumerable<{{extension.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Id> ids) =>
                _locator.LocateMany(ids).Select(e => ({{extension.CSharpFriendlyFullName}})e);
            """, indentation: 1)}}
        }

        """, indentation: 1)}}
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
            {{ForEach(_locatableExtensions, extension => $$"""
                {{If(IsAsync(extension), () => $$"""
                services.AddSingleton<ILocator<{{extension.CSharpFriendlyFullName}}>, {{extension.Name}}Locator>();
                services.AddSingleton<IAsyncLocator<{{extension.CSharpFriendlyFullName}}>, {{extension.Name}}Locator>();
                """, @else: () => $$"""
                services.AddSingleton<ILocator<{{extension.CSharpFriendlyFullName}}>, {{extension.Name}}Locator>();
                """, indentation: 1)}}
            """, indentation: 2)}}
            }
        }
    """;

    bool IsAsync(TypeModelMembers extension) =>
        LocatableType(extension).TryGetMetadata(out var metadata) &&
        metadata.TryGet<LocatableAttribute>(out var locatable) &&
        locatable.IsAsync;

    TypeModel LocatableType(TypeModelMembers extension) =>
        _domain.Types[extension.Get<LocatableExtensionAttribute>().LocatableType];
}