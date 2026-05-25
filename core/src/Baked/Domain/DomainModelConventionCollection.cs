using Baked.Domain.Configuration;

namespace Baked.Domain;

public class DomainModelConventionCollection(DomainModelBuilderOptions _options)
    : List<(IDomainModelConvention Convention, Order Order)>, IDomainModelConventionCollection
{
    int DefaultLevelIndex => !_options.ConventionLevels.Any() ? 0 : _options.ConventionLevels.IndexOf(_options.DefaultLevel);

    void IDomainModelConventionCollection.Add(IDomainModelConvention convention, Order? order)
    {
        if (!order.HasValue)
        {
            order = Order.FromLevel(_options.DefaultLevel);
        }

        var levelIndex = !_options.ConventionLevels.Any() ? 0 : _options.ConventionLevels.IndexOf(order.Value.Level ?? _options.DefaultLevel);
        order.Value.SetValue((levelIndex - DefaultLevelIndex) * Order.OrderSpan);

        Add((convention, order.Value));
    }
}