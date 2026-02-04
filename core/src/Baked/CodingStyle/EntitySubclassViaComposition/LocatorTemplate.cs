using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

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

        {{ForEach(_entitySubclassTypes, s => $$"""
        public class {{s.SubclassType.Name}}Locator({{s.QueryType.CSharpFriendlyFullName}} _query)
            : ILocator<{{s.SubclassType.CSharpFriendlyFullName}}>
        {
            public {{s.SubclassType.CSharpFriendlyFullName}} Locate(Baked.Business.Id id, bool throwNotFound) =>
                ({{s.SubclassType.CSharpFriendlyFullName}})_query.{{s.UniqueMethod.Name}}({{Parameter(s.UniqueParameter)}}, throwNotFound: throwNotFound);

            public IEnumerable<{{s.SubclassType.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Baked.Business.Id> ids) =>
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
            {{ForEach(_entitySubclassTypes, s => $$"""
                services.AddSingleton<ILocator<{{s.SubclassType.CSharpFriendlyFullName}}>, {{s.SubclassType.Name}}Locator>();
            """)}}
            }
        }
    """;

    string Parameter(ParameterModel parameter)
    {
        if (parameter.ParameterType.IsEnum)
        {
            return $$"""({{parameter.ParameterType.CSharpFriendlyFullName}})Enum.Parse<{{parameter.ParameterType.CSharpFriendlyFullName}}>($"{id}")""";
        }

        return """${id}""";
    }
}