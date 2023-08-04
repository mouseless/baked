using Do.Business;
using Do.Test.Business.Default;

namespace Do;

public static class DefaultBusinessExtensions
{
    public static DefaultBusinessFeature Default(this BusinessConfigurator source) => new();
}
