using Baked.Domain.Configuration;

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
            var calculatedOrder = order
                .WithBase(order.Base ?? orderMatrix.FallbackBase(convention))
                .WithLevel(order.Level ?? orderMatrix.FallbackLevel(convention))
                .WithExtension(order.Extension ?? orderMatrix.FallbackExtension(convention))
                .Calculate(orderMatrix.Levels, orderMatrix.DefaultConventionLevel);

            Add((convention, calculatedOrder));
        });

    public class OrderMatrix
    {
        const string BASE_DEFAULT = "BASE_DEFAULT";
        const string LEVEL_DEFAULT = "LEVEL_DEFAULT";
        const string EXTENSION_DEFAULT = "EXTENSION_DEFAULT";

        readonly string _defaultConventionLevel = $"{BASE_DEFAULT}.{LEVEL_DEFAULT}.{EXTENSION_DEFAULT}";
        readonly IReadOnlyDictionary<string, int> _levels = default!;
        readonly Func<IDomainModelConvention, string> _fallbackBase = default!;
        readonly Func<IDomainModelConvention, string> _fallbackLevel = default!;
        readonly Func<IDomainModelConvention, string> _fallbackExtension = default!;

        public OrderMatrix(ConventionOrderMatrixOptions options, string? defaultConventionLevel)
        {
            _defaultConventionLevel = defaultConventionLevel ?? _defaultConventionLevel;
            _levels = BuildLevels(options);

            _fallbackBase = options.FallbackBase ?? (_ => options.Bases.First());
            _fallbackLevel = options.FallbackLevel ?? (_ => options.Levels.First());
            _fallbackExtension = options.FallbackExtension ?? (_ => options.Extensions.First());
        }

        public string DefaultConventionLevel => _defaultConventionLevel;
        public IReadOnlyDictionary<string, int> Levels => _levels;
        public Func<IDomainModelConvention, string> FallbackBase => _fallbackBase;
        public Func<IDomainModelConvention, string> FallbackLevel => _fallbackLevel;
        public Func<IDomainModelConvention, string> FallbackExtension => _fallbackExtension;

        static Dictionary<string, int> BuildLevels(ConventionOrderMatrixOptions options)
        {
            if (!options.Bases.Any()) { options.Bases.Add(BASE_DEFAULT); }
            if (!options.Levels.Any()) { options.Levels.Add(LEVEL_DEFAULT); }
            if (!options.Extensions.Any()) { options.Extensions.Add(EXTENSION_DEFAULT); }

            var levels = new List<string>();
            foreach (var @base in options.Bases)
            {
                foreach (var extension in options.Extensions)
                {
                    foreach (var level in options.Levels)
                    {
                        levels.Add($"{@base}.{level}.{extension}");
                    }
                }
            }

            return levels.Select((name, index) => (name, index))
                .ToDictionary(x => x.name, x => x.index);
        }
    }
}