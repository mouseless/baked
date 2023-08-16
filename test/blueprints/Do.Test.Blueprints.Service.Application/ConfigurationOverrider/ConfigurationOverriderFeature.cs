using Do.Architecture;

namespace Do.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAutomapping(mapping =>
        {
            mapping.MemberIsId.Clear();
            mapping.MemberIsId.Add(member => member.PropertyType == typeof(Guid) && member.Name == "EntityId");
        });
    }
}
