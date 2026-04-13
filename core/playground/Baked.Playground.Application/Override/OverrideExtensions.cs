using Baked.Architecture;

namespace Baked;

public static class OverrideExtensions
{
    extension(List<IFeature> features)
    {
        public void AddOverrides()
        {
            features.Add(new Playground.Override.DataAccess.MappingsDataAccessOverrideFeature());
            features.Add(new Playground.Override.Domain.AttributeExportOverrideFeature());
            features.Add(new Playground.Override.Domain.AuthenticationSamplesDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.CacheSamplesDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.ChildDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.FormSampleDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.CustomAttributeDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.DocumentationSamplesDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.EntityDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.EntityWithAssignedIdDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.EntityWithAutoIncrementIdDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.ExceptionSamplesDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.ILocatableDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.OverrideSamplesDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.ParentDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.ReportPageSampleDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.RouteSampleDomainOverrideFeature());
            features.Add(new Playground.Override.Domain.TestPageDomainOverrideFeature());
            features.Add(new Playground.Override.RestApi.MultiDocumentRestApiOverrideFeature());
            features.Add(new Playground.Override.RestApi.ExternalSecurityRestApiOverrideFeature());
            features.Add(new Playground.Override.Runtime.ServicesRuntimeOverrideFeature());
            features.Add(new Playground.Override.Ui.PluginSampleUiOverrideFeature());
        }
    }
}