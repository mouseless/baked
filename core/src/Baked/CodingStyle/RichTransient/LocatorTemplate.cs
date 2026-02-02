using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked.CodingStyle.RichTransient;

public class LocatorTemplate
    : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
        ];

    List<TypeModel> _richTransientTypes = [];
    List<(string Service, string Implementation)> _generatedServices = [];

    public LocatorTemplate(DomainModel domain)
    {
        foreach (var item in domain.Types.Having<RichTransientAttribute>())
        {
            if (!item.GetMembers().TryGet<LocatableAttribute>(out var locatable)) { continue; }

            _richTransientTypes.Add(item);
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

    namespace RichTransient;
    {{ForEach(_richTransientTypes, r => $$"""

    public class {{r.Name}}Locator({{Factory(r)}})
        : {{LocatorTypeName(r)}}
    {
        public {{ReturnType(r)}} Locate(Id id, bool _) =>
            _new{{r.Name}}().With(id);

        public IEnumerable<{{ReturnType(r)}}> LocateMany(IEnumerable<Id> ids) =>
            ids.Select(id => _new{{r.Name}}().With(id));
    }
    """
    )}}
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
            {{ForEach(_generatedServices, (item) => $$"""
            services.AddSingleton<{{item.Implementation}}>();
            services.AddSingleton<{{item.Service}}, {{item.Implementation}}>(forward: true);
            """)}}
        }
    }
    """;

    string ReturnType(TypeModel typeModel) =>
        typeModel.GetMetadata().TryGet<LocatableAttribute>(out var locatable) && locatable.IsAsync
            ? $$"""Task<{{typeModel.CSharpFriendlyFullName}}>"""
            : $$"""{{typeModel.CSharpFriendlyFullName}}""";
    string Factory(TypeModel typeModel) =>
        $$"""Func<{{typeModel.CSharpFriendlyFullName}}> _new{{typeModel.Name}}""";
    string LocatorTypeName(TypeModel richTransient) =>
        $$"""ILocator<{{ReturnType(richTransient)}}>""";
    string ImplementatonTypeName(TypeModel richTransient) =>
        $$"""RichTransient.{{richTransient.Name}}Locator""";
}