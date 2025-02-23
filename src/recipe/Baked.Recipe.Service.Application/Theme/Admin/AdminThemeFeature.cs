using Baked.Architecture;
using Baked.Ui;

namespace Baked.Theme.Admin;

public class AdminThemeFeature(Func<string, string> _defaultPageName)
    : IFeature<ThemeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ComponentDescriptorAttribute<Detail>>();
        });

        configurator.ConfigurePageDescriptors(components =>
        {
            configurator.UsingDomainModel(domain =>
            {
                components.AddPages<Detail>(domain.Types, _defaultPageName);
            });
        });
    }
}