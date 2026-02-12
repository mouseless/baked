using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Orm.AutoMap;

public class ManyToOneFetcherTemplate(DomainModel _domain)
    : CodeTemplateBase
{
    public static string[] GlobalUsings =
        [
            "Baked.Orm",
            "Baked.Runtime",
            "FluentNHibernate",
            "FluentNHibernate.Diagnostics",
            "Microsoft.Extensions.DependencyInjection",
            "NHibernate.Linq"
        ];

    protected override IEnumerable<string> Render() => [
        ServiceAdder(),
        .. _domain
            .Types
            .Having<EntityAttribute>()
            .Where(type => type.HasMembers())
            .Select(type => type.GetMembers())
            .Select(ManyToOneFetcher)
    ];

    string ServiceAdder() => $$"""
        namespace AutoMapOrmFeature;

        public class ServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_domain.Types.Having<EntityAttribute>(), entity => $$"""
                services.AddSingleton<IManyToOneFetcher<{{entity.CSharpFriendlyFullName}}>, {{entity.Name}}ManyToOneFetcher>();
            """)}}
            }
        }
    """;

    string ManyToOneFetcher(TypeModelMembers entity) => $$"""
        namespace AutoMapOrmFeature;

        public class {{entity.Name}}ManyToOneFetcher : IManyToOneFetcher<{{entity.CSharpFriendlyFullName}}>
        {
            public IQueryable<{{entity.CSharpFriendlyFullName}}> Fetch(IQueryable<{{entity.CSharpFriendlyFullName}}> query)
            {
            {{ForEach(entity.Properties.Where(p => p.PropertyType.TryGetMetadata(out var property) && property.Has<EntityAttribute>()), property => $$"""
                query = query.Fetch(e => e.{{property.Name}});
            """)}}

                return query;
            }
        }
    """;
}