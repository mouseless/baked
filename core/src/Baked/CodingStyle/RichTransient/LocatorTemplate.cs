using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked.CodingStyle.RichTransient;

public class LocatorTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
        ];

    List<TypeModel> _richTransients = [];

    public LocatorTemplate(DomainModel domain)
    {
        foreach (var item in domain.Types.Having<RichTransientAttribute>())
        {
            if (!item.GetMembers().TryGet<LocatableAttribute>(out var locatable)) { continue; }

            _richTransients.Add(item);
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

    namespace RichTransientCodingStyleFeature;

    {{ForEach(_richTransients, richTransient => $$"""
    public class {{richTransient.Name}}Locator(Func<{{richTransient.CSharpFriendlyFullName}}> _new{{richTransient.Name}})
        {{If(richTransient.GetMetadata().TryGet<LocatableAttribute>(out var locatable) && locatable.IsAsync,
            () => $": IAsyncLocator<{richTransient.CSharpFriendlyFullName}>",
        @else:
            () => $": ILocator<{richTransient.CSharpFriendlyFullName}>"
        )}}
    {
        {{If(richTransient.GetMetadata().TryGet<LocatableAttribute>(out var l) && l.IsAsync,
            () => $$"""
            public async Task<{{richTransient.CSharpFriendlyFullName}}> LocateAsync(Id id, bool _) =>
                await _new{{richTransient.Name}}().With(id);

            public async Task<IEnumerable<{{richTransient.CSharpFriendlyFullName}}>> LocateManyAsync(IEnumerable<Id> ids) =>
                await Task.WhenAll(ids.Select(id => _new{{richTransient.Name}}().With(id)));

            public {{richTransient.CSharpFriendlyFullName}} Locate(Id id, bool _) =>
                LocateAsync(id, _).GetAwaiter().GetResult();

            public IEnumerable<{{richTransient.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Id> ids) =>
                LocateManyAsync(ids).GetAwaiter().GetResult();
            """,
        @else:
            () => $$"""
            public {{richTransient.CSharpFriendlyFullName}} Locate(Id id, bool _) =>
                _new{{richTransient.Name}}().With(id);

            public IEnumerable<{{richTransient.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Id> ids) =>
                ids.Select(id => _new{{richTransient.Name}}().With(id));
            """
        )}}
       
    }
    """)}}
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
            {{ForEach(_richTransients, (richTransient) => $$"""
                {{If(richTransient.GetMetadata().TryGet<LocatableAttribute>(out var locatable) && locatable.IsAsync,
                    () => $"services.AddSingleton<IAsyncLocator<{richTransient.CSharpFriendlyFullName}>, {richTransient.Name}Locator>();",
                @else:
                    () => $"services.AddSingleton<ILocator<{richTransient.CSharpFriendlyFullName}>, {richTransient.Name}Locator>();"
                )}}
            """)}}
        }
    }
    """;
}