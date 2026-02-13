using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Orm.AutoMap;

public class ManyToOneFetcherTemplate : CodeTemplateBase
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

    readonly IEnumerable<TypeModelMembers> _entities;

    public ManyToOneFetcherTemplate(DomainModel domain)
    {
        _entities = domain
            .Types
            .Having<EntityAttribute>()
            .Where(type => type.HasMembers())
            .Select(type => type.GetMembers());

        AddReferences(_entities);
    }

    protected override IEnumerable<string> Render() => [
        ServiceAdder(),
        .. _entities.Select(ManyToOneFetcher)
    ];

    string ServiceAdder() => $$"""
        namespace AutoMapOrmFeature;

        public class ServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_entities, entity => $$"""
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