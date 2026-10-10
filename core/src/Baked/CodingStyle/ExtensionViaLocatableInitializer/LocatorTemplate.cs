using Baked.Buildtime;
using Baked.Business;
using Baked.Domain.Model;

namespace Baked.CodingStyle.ExtensionViaLocatableInitializer;

public class LocatorTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.TypeBasedId",
            "Baked.Orm"
        ];

    readonly DomainModel _domain;
    readonly List<TypeModelMembers> _locatableExtensions = [];

    public LocatorTemplate(DomainModel domain)
    {
        _domain = domain;
        foreach (var item in _domain.Types.Having<LocatableExtension>())
        {
            if (!item.TryGetMembers(out var members)) { continue; }
            if (!members.TryGet<Locatable>(out var _)) { continue; }

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

        namespace ExtensionViaLocatableInitializerCodingStyleFeature;

        {{ForEach(_locatableExtensions, extension => $$"""
        public class {{extension.Name}}Locator(
            Func<{{extension.CSharpFriendlyFullName}}> _new{{extension.Name}},
            I{{If(IsAsync(extension), () => "Async")}}Locator<{{LocatableType(extension).CSharpFriendlyFullName}}> _locator
        ) : I{{If(IsAsync(extension), () => "Async")}}Locator<{{extension.CSharpFriendlyFullName}}>
        {
            {{If(IsAsync(extension), () => $$"""
            public async Task<{{extension.CSharpFriendlyFullName}}> LocateAsync(Id id, bool throwNotFound) =>
                {{New(extension, "await _locator.LocateAsync(id, throwNotFound: throwNotFound)")}};

            public async Task<IEnumerable<{{extension.CSharpFriendlyFullName}}>> LocateManyAsync(IEnumerable<Id> ids) =>
                (await _locator.LocateManyAsync(ids)).Select(e => {{New(extension, "e")}});

            public {{extension.CSharpFriendlyFullName}} Locate(Id id, bool throwNotFound) =>
                {{New(extension, "_locator.Locate(id, throwNotFound: throwNotFound)")}};

            public LazyLocatable<{{extension.CSharpFriendlyFullName}}> LocateLazily(Id id)
            {
                var result = _locator.LocateLazily(id);

                return new({{New(extension, "result.Value")}}, result.Initialize);
            }

            public IEnumerable<{{extension.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Id> ids) =>
                _locator.LocateMany(ids).Select(e => {{New(extension, "e")}});
            """,
            @else: () => $$"""
            public {{extension.CSharpFriendlyFullName}} Locate(Id id, bool throwNotFound) =>
                {{New(extension, "_locator.Locate(id, throwNotFound: throwNotFound)")}};

            public LazyLocatable<{{extension.CSharpFriendlyFullName}}> LocateLazily(Id id)
            {
                var result = _locator.LocateLazily(id);

                return new({{New(extension, "result.Value")}}, result.Initialize);
            }

            public IEnumerable<{{extension.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Id> ids) =>
                _locator.LocateMany(ids).Select(e => {{New(extension, "e")}});
            """, indentation: 1)}}
        }

        """, indentation: 1)}}
    """;

    string ServiceAdder() => $$"""
        using Baked;
        using Baked.Domain;
        using Baked.Runtime;
        using Microsoft.Extensions.DependencyInjection;

        namespace ExtensionViaLocatableInitializerCodingStyleFeature;

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
        metadata.TryGet<Locatable>(out var locatable) &&
        locatable.IsAsync;

    TypeModel LocatableType(TypeModelMembers extension) =>
        _domain.Types[extension.Get<LocatableExtension>().LocatableType];

    string New(TypeModelMembers extension, string expression) =>
        $$"""_new{{extension.Name}}().{{extension.FirstMethod<Initializer>().Name}}({{expression}})""";
}