using Baked.Architecture;
using Baked.Business;
using Baked.CodingStyle.EntitySubclassViaComposition;
using Baked.Orm;
using Baked.Ui;
using Humanizer;

namespace Baked.Theme.Admin;

public class AdminThemeFeature(Func<string, string> _defaultPageName)
    : IFeature<ThemeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ComponentDescriptorAttribute<Detail>>();

            builder.Conventions.AddTypeMetadata(
                attribute: context => new ComponentDescriptorAttribute<Detail>(new() { Title = context.Type.Name.Humanize() }),
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<LocatableAttribute>() &&
                    !members.Has<EntityAttribute>() && // temp filter
                    !members.Has<EntitySubclassAttribute>() && // temp filter
                    members.Properties.Any(p => p.IsPublic),
                order: 20
            );

            builder.Conventions.Add(new FillDetailComponentConvention(), order: 40);
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