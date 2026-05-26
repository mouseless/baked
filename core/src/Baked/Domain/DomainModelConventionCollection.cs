using Baked.Buildtime.Diagnostics;
using Baked.Domain.Configuration;

namespace Baked.Domain;

public class DomainModelConventionCollection(DomainModelBuilderOptions _options)
    : List<(IDomainModelConvention Convention, int Order)>, IDomainModelConventionCollection
{
    readonly IReadOnlyDictionary<string, int> _levels = _options.ConventionLevels
            .Select((name, index) => new { name, index })
            .ToDictionary(x => x.name, x => x.index);

    public Action<DiagnosticsResult>? OnCollectionFinalized => throw new NotImplementedException();

    void IDomainModelConventionCollection.Add(IDomainModelConvention convention, Order order) =>
            Add((convention, order.Calculate(_levels, _options.DefaultConventionLevel)));
}