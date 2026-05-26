using Baked.Domain.Configuration;

namespace Baked.Domain;

public class DomainModelConventionCollection(DomainModelBuilderOptions _options)
    : List<(IDomainModelConvention Convention, int Order)>, IDomainModelConventionCollection
{
    readonly IReadOnlyDictionary<string, int> _levels = _options.ConventionLevels
            .Select((name, index) => new { name, index })
            .ToDictionary(x => x.name, x => x.index);

    void IDomainModelConventionCollection.Add(IDomainModelConvention convention, Order order) =>
        Diagnostics.Current.Diagnose(() =>
        {
            Add((convention, order.Calculate(_levels, _options.DefaultConventionLevel)));
        });
}