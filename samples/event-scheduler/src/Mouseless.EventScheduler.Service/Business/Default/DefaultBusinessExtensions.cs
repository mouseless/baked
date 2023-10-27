using Do.Business;
using EventScheduler.Business.Default;

namespace EventScheduler;

public static class DefaultBusinessExtensions
{
    public static DefaultBusinessFeature Default(this BusinessConfigurator _) => new();
}
