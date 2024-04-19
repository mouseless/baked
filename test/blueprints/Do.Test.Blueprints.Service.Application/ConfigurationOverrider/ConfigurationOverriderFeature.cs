using Do.Architecture;
using Do.RestApi.Model;
using Do.Test.ExceptionHandling;
using Do.Test.Orm;

namespace Do.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
            model.Override<Entity>(x => x.Map(e => e.Unique).Column("UniqueString").Unique());
        });

        configurator.ConfigureApiModel(apiModel =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            apiModel.GetController<ExceptionSamples>().Action[nameof(ExceptionSamples.Throw)].Parameter["handled"].From = ParameterModelFrom.Query;

            apiModel.GetController<Entities>().AddSingleById<Entity>(domainModel);
        });
    }
}