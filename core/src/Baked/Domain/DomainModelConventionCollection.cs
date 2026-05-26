using Baked.Domain.Configuration;

namespace Baked.Domain;

public class DomainModelConventionCollection(DomainModelBuilderOptions _options)
    : List<(IDomainModelConvention Convention, int Order)>, IDomainModelConventionCollection
{
    int _defaultLevelIndex = _options.DefaultLevel is null ? 0
                : !_options.ConventionLevels.Any() ? 0
                    : _options.ConventionLevels.IndexOf(_options.DefaultLevel);

    void IDomainModelConventionCollection.Add(IDomainModelConvention convention, Order order)
    {
        using (Diagnostics.Start(nameof(DomainModelConventionCollection), onDispose: _options.OnComplete))
        {
            Diagnostics.Current.Diagnose(() =>
            {
                if (order.IsGlobal)
                {
                    Add((convention, order.CalculateValue()));
                }
                else
                {
                    var conventionLevel = order.Level ?? _options.DefaultLevel;
                    var levelIndex = conventionLevel is null ? 0 : _options.ConventionLevels.IndexOf(conventionLevel);
                    levelIndex = levelIndex == -1 ? _defaultLevelIndex : levelIndex;

                    Add((convention, order.WithBase(levelIndex - _defaultLevelIndex).CalculateValue()));
                }
            });
        }
    }
}