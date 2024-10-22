using Baked.Architecture;
using Baked.Reporting;

namespace Baked;

public static class ReportingExtensions
{
    public static void AddReporting(this List<IFeature> features, Func<ReportingConfigurator, IFeature<ReportingConfigurator>> configure) =>
        features.Add(configure(new()));
}