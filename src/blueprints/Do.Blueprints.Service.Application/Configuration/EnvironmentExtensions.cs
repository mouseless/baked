using Do.Database;
using Microsoft.Extensions.Hosting;

namespace Do;

public static class EnvironmentExtensions
{
    public static IDatabaseFeature ForDevelopment(this IDatabaseFeature @default, IDatabaseFeature featureOnDevelopment) =>
        @default.For(Environments.Development, featureOnDevelopment);

    public static IDatabaseFeature ForStaging(this IDatabaseFeature @default, IDatabaseFeature featureOnStaging) =>
        @default.For(Environments.Staging, featureOnStaging);

    public static IDatabaseFeature ForProduction(this IDatabaseFeature @default, IDatabaseFeature featureOnProduction) =>
        @default.For(Environments.Production, featureOnProduction);

    public static IDatabaseFeature For(this IDatabaseFeature @default, string environment, IDatabaseFeature featureOnEnvironment)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment)
        {
            return featureOnEnvironment;
        }

        return @default;
    }
}
