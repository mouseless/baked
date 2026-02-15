using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.CodingStyle.RichTransient;

public class LocatorTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
        ];

    readonly List<TypeModel> _richTransients = [];

    public LocatorTemplate(DomainModel domain)
    {
        foreach (var item in domain.Types.Having<RichTransientAttribute>())
        {
            if (!item.GetMembers().TryGet<LocatableAttribute>(out _)) { continue; }

            _richTransients.Add(item);
        }

        AddReferences(_richTransients);
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

        namespace RichTransientCodingStyleFeature;

        {{ForEach(_richTransients, richTransient => $$"""
        public class {{richTransient.Name}}Locator(Func<{{richTransient.CSharpFriendlyFullName}}> _new{{richTransient.Name}})
            : I{{If(IsAsync(richTransient), () => "Async")}}Locator<{{richTransient.CSharpFriendlyFullName}}>
        {
            {{If(IsAsync(richTransient), () => $$"""
            public async Task<{{richTransient.CSharpFriendlyFullName}}> LocateAsync(Id id, bool _) =>
                await _new{{richTransient.Name}}().With(id);

            public async Task<IEnumerable<{{richTransient.CSharpFriendlyFullName}}>> LocateManyAsync(IEnumerable<Id> ids) =>
                await Task.WhenAll(ids.Select(id => _new{{richTransient.Name}}().With(id)));

            public {{richTransient.CSharpFriendlyFullName}} Locate(Id id, bool _) =>
                LocateAsync(id, _).GetAwaiter().GetResult();

            public LazyLocatable<{{richTransient.CSharpFriendlyFullName}}> LocateLazily(Id id)
            {
                var result = _new{{richTransient.Name}}();

                return new(result, async () => await result.With(id));
            }

            public IEnumerable<{{richTransient.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Id> ids) =>
                LocateManyAsync(ids).GetAwaiter().GetResult();
            """,
            @else: () => $$"""
            public {{richTransient.CSharpFriendlyFullName}} Locate(Id id, bool _) =>
                _new{{richTransient.Name}}().With(id);

            public LazyLocatable<{{richTransient.CSharpFriendlyFullName}}> LocateLazily(Id id)
            {
                var result = _new{{richTransient.Name}}();

                return new(result, () => Task.FromResult(result.With(id)));
            }

            public IEnumerable<{{richTransient.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Id> ids) =>
                ids.Select(id => _new{{richTransient.Name}}().With(id));
            """, indentation: 1)}}
        }

        """, indentation: 1)}}
    """;

    string ServiceAdder() => $$"""
        using Baked;
        using Baked.Domain;
        using Baked.Runtime;
        using Microsoft.Extensions.DependencyInjection;

        namespace RichTransientCodingStyleFeature;

        public class ServiceServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_richTransients, richTransient => $$"""
                {{If(IsAsync(richTransient), () => $$"""
                services.AddSingleton<ILocator<{{richTransient.CSharpFriendlyFullName}}>, {{richTransient.Name}}Locator>();
                services.AddSingleton<IAsyncLocator<{{richTransient.CSharpFriendlyFullName}}>, {{richTransient.Name}}Locator>();
                """, @else: () => $$"""
                services.AddSingleton<ILocator<{{richTransient.CSharpFriendlyFullName}}>, {{richTransient.Name}}Locator>();
                """, indentation: 1)}}
            """, indentation: 2)}}
            }
        }
    """;

    bool IsAsync(TypeModel type) =>
        type.TryGetMetadata(out var metadata) &&
        metadata.TryGet<LocatableAttribute>(out var locatable) &&
        locatable.IsAsync;
}