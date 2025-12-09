using Baked.Architecture;

namespace Baked;

public static class OverrideExtensions
{
    public static void AddOverrides(this List<IFeature> features)
    {
        features.Add(new Test.Override.DataAccess.MappingsDataAccessOverrideFeature());
        features.Add(new Test.Override.Domain.CustomAttributeDomainOverrideFeature());
        features.Add(new Test.Override.RestApi.MultiDocumentRestApiOverrideFeature());
        features.Add(new Test.Override.RestApi.RoutesRestApiOverrideFeature());
        features.Add(new Test.Override.RestApi.ExternalSecurityRestApiOverrideFeature());
        features.Add(new Test.Override.Runtime.ServicesRuntimeOverrideFeature());
        features.Add(new Test.Override.Ui.PluginSampleUiOverrideFeature());
        features.Add(new Test.Override.Ui.CacheSamplesUiOverrideFeature());
        features.Add(new Test.Override.Ui.DataTableSampleUiOverrideFeature());
        features.Add(new Test.Override.Ui.ReportPageSampleUiOverrideFeature());
        features.Add(new Test.Override.Ui.TestPageUiOverrideFeature());
    }
}