using Baked.Domain.Configuration;

using static Baked.Domain.Configuration.DomainModelBuilderOptions;

namespace Baked.Domain;

public class DomainModelConventionCollection(DomainModelBuilderOptions _options)
    : List<(IDomainModelConvention Convention, int Order)>, IDomainModelConventionCollection
{
    readonly Lazy<IReadOnlyDictionary<string, int>> _levels = new(() => BuildLevels(_options.ConventionMatrix));

    void IDomainModelConventionCollection.Add(IDomainModelConvention convention, Order order) =>
        Diagnostics.Current.Diagnose(() =>
        {
            var calculatedOrder = order
                .WithBase(order.Base ?? _options.ConventionMatrix.FallbackBase(convention))
                .WithLevel(order.Level ?? _options.ConventionMatrix.FallbackLevel(convention))
                .WithExtension(order.Extension ?? _options.ConventionMatrix.FallbackExtension(convention))
                .Calculate(_levels.Value, _options.DefaultConventionLevel);

            Add((convention, calculatedOrder));
        });

    static IReadOnlyDictionary<string, int> BuildLevels(ConventionMatrixOptions options)
    {
        var result = new List<string>();
        foreach (var @base in options.Bases)
        {
            foreach (var extension in options.Extensions)
            {
                foreach (var level in options.Levels)
                {
                    result.Add($"{@base}.{level}.{extension}");
                }
            }
        }

        return result.Select((name, index) => (name, index))
            .ToDictionary(x => x.name, x => x.index);
    }
}