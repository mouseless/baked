using Baked.Domain.Configuration;

namespace Baked.Domain;

public class DomainModelConventionCollection(DomainModelBuilderOptions _options)
    : List<(IDomainModelConvention Convention, int Order)>, IDomainModelConventionCollection
{
    readonly Lazy<IReadOnlyDictionary<string, int>> _levels = new(() =>
        _options.ConventionLevels
            .Select((name, index) => (name, index))
            .ToDictionary(x => x.name, x => x.index)
    );

    void IDomainModelConventionCollection.Add(IDomainModelConvention convention, Order order)
    {
        Diagnostics.Current.Diagnose(() =>
        {
            Add((convention, order.Calculate(_levels.Value, _options.DefaultConventionLevel)));
        });
    }
}