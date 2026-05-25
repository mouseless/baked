using Baked.Domain.Configuration;

namespace Baked.Domain;

public class DomainModelConventionCollection(DomainModelBuilderOptions _options)
    : List<(IDomainModelConvention Convention, int Order)>, IDomainModelConventionCollection
{
    int? _defaultLevelIndex;

    void IDomainModelConventionCollection.Add(IDomainModelConvention convention, Order? order)
    {
        if (!_defaultLevelIndex.HasValue)
        {
            _defaultLevelIndex = string.IsNullOrEmpty(_options.DefaultLevel) ? 0
                : !_options.ConventionLevels.Any() ? 0 : _options.ConventionLevels.IndexOf(_options.DefaultLevel);
        }

        if (!order.HasValue)
        {
            order = _options.DefaultLevel is null ? new() : Order.FromLevel(_options.DefaultLevel);
        }

        var level = order.Value.Level ?? _options.DefaultLevel;
        var levelIndex = level is null ? 0 : _options.ConventionLevels.IndexOf(level);
        levelIndex = levelIndex == -1 ? _defaultLevelIndex.Value : levelIndex;
        order = order.Value.SetBase((levelIndex - _defaultLevelIndex.Value) * Order.OrderSpan);

        Add((convention, order.Value));
    }
}