using Baked.Domain.Configuration;
using NHibernate.Util;
using Spectre.Console;

using static Baked.Domain.Configuration.DomainModelBuilderOptions;

namespace Baked.Domain;

public class DomainModelConventionCollection(DomainModelBuilderOptions _options)
    : List<(IDomainModelConvention Convention, int Order)>, IDomainModelConventionCollection
{
    readonly Lazy<OrderMatrix> _orderMatrix = new(() => new(_options.ConventionOrderMatrix, _options.DefaultConventionLevel));

    void IDomainModelConventionCollection.Add(IDomainModelConvention convention, Order order) =>
        Diagnostics.Current.Diagnose(() =>
        {
            var orderMatrix = _orderMatrix.Value;
            order = order
                .WithBase(order.Base ?? orderMatrix.FallbackBase(convention))
                .WithLevel(order.Level ?? orderMatrix.FallbackLevel(convention))
                .WithExtension(order.Extension ?? orderMatrix.FallbackExtension(convention));
            var calculation = order.Calculate(orderMatrix.Levels, orderMatrix.DefaultConventionLevel);
            if (calculation is null)
            {
                Diagnostics.Current.ReportInfo(
                    $"Convention '{Markup.Escape($"{convention}")}' is skipped due to unrecognized order: '{order}'." +
                    " Make sure convention order matrix contains the required base, level and/or extension."
                );

                return;
            }

            Add((convention, calculation.Value));
        });

    class OrderMatrix(ConventionOrderMatrixOptions options, string? defaultConventionLevel)
    {
        const string BASE_DEFAULT = "Default";
        const string LEVEL_DEFAULT = "Default";
        const string EXTENSION_DEFAULT = "Default";
        const string DEFAULT_LEVEL = $"{BASE_DEFAULT}.{LEVEL_DEFAULT}.{EXTENSION_DEFAULT}";

        public string DefaultConventionLevel { get; } = defaultConventionLevel ?? DEFAULT_LEVEL;
        public IReadOnlyDictionary<string, int> Levels { get; } = BuildLevels(options);
        public Func<IDomainModelConvention, string> FallbackBase { get; } = options.FallbackBase ?? (_ => options.Bases.FirstOrDefault() ?? BASE_DEFAULT);
        public Func<IDomainModelConvention, string> FallbackLevel { get; } = options.FallbackLevel ?? (_ => options.Levels.FirstOrDefault() ?? LEVEL_DEFAULT);
        public Func<IDomainModelConvention, string> FallbackExtension { get; } = options.FallbackExtension ?? (_ => options.Extensions.FirstOrDefault() ?? EXTENSION_DEFAULT);

        static Dictionary<string, int> BuildLevels(ConventionOrderMatrixOptions options)
        {
            var bases = !options.Bases.Any() ? [BASE_DEFAULT] : options.Bases;
            var levels = !options.Levels.Any() ? [LEVEL_DEFAULT] : options.Levels;
            var extensions = !options.Extensions.Any() ? [EXTENSION_DEFAULT] : options.Extensions;

            var calculatedLevels = new List<string>();
            foreach (var @base in bases)
            {
                foreach (var extension in extensions)
                {
                    foreach (var level in levels)
                    {
                        calculatedLevels.Add($"{@base}.{level}.{extension}");
                    }
                }
            }

            if (calculatedLevels.Count > 1)
            {
                calculatedLevels.Insert(0, DEFAULT_LEVEL);
            }

            return calculatedLevels.Select((name, index) => (name, index))
                .ToDictionary(x => x.name, x => x.index);
        }
    }
}