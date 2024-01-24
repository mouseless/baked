using Do.Business;
using Do.Business.Default;

namespace Do;

public static class DefaultBusinessExtensions
{
    public static DefaultBusinessFeature Default(this BusinessConfigurator _) => new();
}
