using Do.Architecture;
using Do.Business;
using System.Reflection;

namespace Do.Test.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddTransientWithFactory<Entity>();
            services.AddSingleton<Entities>();
            services.AddSingleton<Singleton>();
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.AddEntityAssembly(typeof(Entity).Assembly);
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(Assembly.GetExecutingAssembly() ?? throw new NotSupportedException("Executing assembly should not be null")));
        });
    }
}
