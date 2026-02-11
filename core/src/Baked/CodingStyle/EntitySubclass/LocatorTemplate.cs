using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked.CodingStyle.EntitySubclass;

public class LocatorTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
            "Baked.Orm"
        ];

    List<(TypeModel SubclassType, TypeModel QueryType, MethodModel UniqueMethod, ParameterModel UniqueParameter)> _entitySubclassTypes = [];

    public LocatorTemplate(DomainModel domain)
    {
        foreach (var item in domain.Types.Having<EntitySubclassAttribute>())
        {
            if (!item.GetMembers().TryGet<LocatableAttribute>(out var _)) { continue; }

            var entitySubclassType = item;
            if (!entitySubclassType.TryGetSubclassName(out var subclassName)) { continue; }
            if (!entitySubclassType.TryGetEntityTypeFromSubclass(domain, out var entityType)) { continue; }
            if (!entityType.TryGetQueryType(domain, out var queryType)) { continue; }
            if (!queryType.TryGetMembers(out var queryMembers)) { continue; }

            var singleByUniqueMethod = queryMembers.Methods.FirstOrDefault(m => m.Name.StartsWith("SingleBy"));
            if (singleByUniqueMethod is null) { continue; }

            var uniqueParameter = singleByUniqueMethod.DefaultOverload.Parameters.First();
            if (!uniqueParameter.ParameterType.IsEnum && !uniqueParameter.ParameterType.Is<string>()) { continue; }

            _entitySubclassTypes.Add((item, queryType, singleByUniqueMethod, uniqueParameter));
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

        namespace EntitySubclassViaCompositionCodingStyleFeature;

        {{ForEach(_entitySubclassTypes, context => $$"""
        public class {{context.SubclassType.Name}}Locator({{context.QueryType.CSharpFriendlyFullName}} _query)
            : ILocator<{{context.SubclassType.CSharpFriendlyFullName}}>
        {
            public {{context.SubclassType.CSharpFriendlyFullName}} Locate(Baked.Business.Id id, bool throwNotFound) =>
                ({{context.SubclassType.CSharpFriendlyFullName}})_query.{{context.UniqueMethod.Name}}({{Parameter(context.UniqueParameter)}}, throwNotFound: throwNotFound);

            public ({{context.SubclassType.CSharpFriendlyFullName}}, Task) LocateLazily(Baked.Business.Id _) =>
                throw new InvalidOperationException("`{{context.SubclassType.Name}}` cannot be located lazily, use the actual entity and cast later");

            public IEnumerable<{{context.SubclassType.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Baked.Business.Id> ids) =>
                ids.Select(id => Locate(id, false));
        }
        """)}}
    """;

    string ServiceAdder() => $$"""
        using Baked;
        using Baked.Domain;
        using Baked.Runtime;
        using Microsoft.Extensions.DependencyInjection;

        namespace EntitySubclassViaCompositionCodingStyleFeature;

        public class ServiceServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_entitySubclassTypes, context => $$"""
                services.AddSingleton<ILocator<{{context.SubclassType.CSharpFriendlyFullName}}>, {{context.SubclassType.Name}}Locator>();
            """)}}
            }
        }
    """;

    string Parameter(ParameterModel parameter) =>
        parameter.ParameterType.IsEnum
            ? $$"""({{parameter.ParameterType.CSharpFriendlyFullName}})Enum.Parse<{{parameter.ParameterType.CSharpFriendlyFullName}}>($"{id}")"""
            : "${id}";
}