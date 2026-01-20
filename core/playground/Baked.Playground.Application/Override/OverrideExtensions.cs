using Baked.Architecture;

namespace Baked;

public static class OverrideExtensions
{
    public static void AddOverrides(this List<IFeature> features)
    {
        features.Add(new Playground.Override.DataAccess.MappingsDataAccessOverrideFeature());
        features.Add(new Playground.Override.Domain.CustomAttributeDomainOverrideFeature());
        features.Add(new Playground.Override.Id.MapingIdsOverrideFeature());
        features.Add(new Playground.Override.RestApi.MultiDocumentRestApiOverrideFeature());
        features.Add(new Playground.Override.RestApi.RoutesRestApiOverrideFeature());
        features.Add(new Playground.Override.RestApi.ExternalSecurityRestApiOverrideFeature());
        features.Add(new Playground.Override.Runtime.ServicesRuntimeOverrideFeature());
        features.Add(new Playground.Override.Ui.CacheSamplesUiOverrideFeature());
        features.Add(new Playground.Override.Ui.FormSampleUiOverrideFeature());
        features.Add(new Playground.Override.Ui.ParentUiOverrideFeature());
        features.Add(new Playground.Override.Ui.PluginSampleUiOverrideFeature());
        features.Add(new Playground.Override.Ui.ReportPageSampleUiOverrideFeature());
        features.Add(new Playground.Override.Ui.TestPageUiOverrideFeature());
    }
}