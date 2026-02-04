using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Model;
using Baked.Orm;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class OrmExtensions
{
    public static void AddOrm(this List<IFeature> features, Func<OrmConfigurator, IFeature<OrmConfigurator>> configure) =>
        features.Add(configure(new()));

    public static bool TryGetQueryType(this TypeModel type, DomainModel domain, [NotNullWhen(true)] out TypeModel? queryType)
    {
        if (!type.TryGetEntityAttribute(out var entityAttribute))
        {
            queryType = default;

            return false;
        }

        queryType = domain.Types[entityAttribute.QueryType];

        return true;
    }

    public static bool TryGetEntityType(this TypeModel type, DomainModel domain, [NotNullWhen(true)] out TypeModel? entityType)
    {
        if (!type.TryGetQueryAttribute(out var queryAttribute))
        {
            entityType = default;

            return false;
        }

        entityType = domain.Types[queryAttribute.LocatableType];

        return true;
    }

    public static bool TryGetQueryContextType(this TypeModel type, DomainModel domain, [NotNullWhen(true)] out TypeModel? queryContextType)
    {
        if (!type.TryGetEntityAttribute(out _))
        {
            queryContextType = default;

            return false;
        }

        queryContextType = domain.Types[domain.Types[typeof(IQueryContext<>)].MakeGenericTypeId(type)];

        return true;
    }

    public static bool TryGetQueryAttribute(this TypeModel type, [NotNullWhen(true)] out QueryAttribute? queryAttribute)
    {
        queryAttribute = default;

        return
            type.TryGetMetadata(out var metadata) &&
            metadata.TryGet(out queryAttribute);
    }

    public static bool TryGetEntityAttribute(this TypeModel type, [NotNullWhen(true)] out EntityAttribute? entityAttribute)
    {
        entityAttribute = default;

        return
            type.TryGetMetadata(out var metadata) &&
            metadata.TryGet(out entityAttribute);
    }

    public static Id AnId(this Stubber _,
        string? starts = default
    )
    {
        starts ??= string.Empty;

        const string template = "4d13bbe0-07a4-4b64-9d31-8fef958fbef1";

        return Id.Parse($"{starts}{template[starts.Length..]}");
    }

    public static void ShouldBeDeleted(this object @object) =>
        Spec
          .StartContextStatic
          .GetServiceProvider()
          .UsingCurrentScope()
          .GetRequiredService<ISession>()
          .Contains(@object)
          .ShouldBeFalse($"{@object} should've been deleted, but it's STILL in the session");

    public static void ShouldBeInserted(this object @object) =>
        Spec
          .StartContextStatic
          .GetServiceProvider()
          .UsingCurrentScope()
          .GetRequiredService<ISession>()
          .Contains(@object)
          .ShouldBeTrue($"{@object} should've been inserted, but it's NOT in the session");
}