using Microsoft.Extensions.Hosting;
using Do.Architecture;

namespace Do;

public static class EnvironmentExtensions
{
    public static IFeature ForDevelopment(this IFeature @default, IFeature featureOnDevelopment) =>
      @default.For(Environments.Development, featureOnDevelopment);

    public static IFeature ForStaging(this IFeature @default, IFeature featureOnStaging) =>
      @default.For(Environments.Staging, featureOnStaging);

    public static IFeature ForProduction(this IFeature @default, IFeature featureOnProduction) =>
      @default.For(Environments.Production, featureOnProduction);

    public static IFeature For(this IFeature @default, string environment, IFeature featureOnEnvironment)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment)
        {
            return featureOnEnvironment;
        }

        return @default;
    }
}
