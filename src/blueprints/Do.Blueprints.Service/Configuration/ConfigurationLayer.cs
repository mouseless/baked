using Do.Architecture;
using Microsoft.Extensions.Configuration;

using static Do.Configuration.ConfigurationLayer;

namespace Do.Configuration;

public class ConfigurationLayer : LayerBase<BuildConfiguration>
{
    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContext<IConfigurationBuilder>(Context.GetConfigurationManager());

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildConfiguration();
    }

    public class BuildConfiguration : PhaseBase<ConfigurationManager>
    {
        public BuildConfiguration() : base(PhaseOrder.Earliest) { }

        protected override void Initialize(ConfigurationManager configurationManager)
        {
            Settings.SetConfiguration(configurationManager);
        }
    }
}
