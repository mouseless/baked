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

    namespace RichTransient;

    {{ForEach(_richTransients, richTransient => $$"""
    public class {{richTransient.Name}}Locator(Func<{{richTransient.CSharpFriendlyFullName}}> _new{{richTransient.Name}})
        : ILocator<{{ReturnType(richTransient)}}>
    {
        public {{ReturnType(richTransient)}} Locate(Id id, bool _) =>
            _new{{richTransient.Name}}().With(id);

        public IEnumerable<{{ReturnType(richTransient)}}> LocateMany(IEnumerable<Id> ids) =>
            ids.Select(id => _new{{richTransient.Name}}().With(id));
    }
    """)}}
    """;

    string ServiceAdder() => $$"""    
    using Baked;
    using Baked.Domain;
    using Baked.Runtime;
    using Microsoft.Extensions.DependencyInjection;

    namespace RichTransient;

    public class ServiceServiceAdder : IServiceAdder
    {
        public void AddServices(IServiceCollection services)
        {
            {{ForEach(_richTransients, (richTransient) => $$"""
            services.AddSingleton<ILocator<{{ReturnType(richTransient)}}>, {{richTransient.Name}}Locator>();
            """)}}
        }
    }
    """;

    string ReturnType(TypeModel typeModel) =>
        typeModel.GetMetadata().TryGet<LocatableAttribute>(out var locatable) && locatable.IsAsync
            ? $$"""Task<{{typeModel.CSharpFriendlyFullName}}>"""
            : $$"""{{typeModel.CSharpFriendlyFullName}}""";
}