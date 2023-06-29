namespace Do.Architecture;

public class Application : IRunnable
{
    List<string> Phases { get; } = new();
    public List<ILayer> Layers { get; } = new();
    public List<IFeature> Features { get; } = new();

    void IRunnable.Run()
    {
        /*
        // this context will have service collection, application builder, application etc.
        var context = new ApplicationContext();
        */

        foreach (var layer in Layers)
        {
            layer.Configure(Phases);
        }

        /*
        // apply phases
        foreach (var phase in Phases)
        {
            using (phase.Begin(context))
            {
        */
        foreach (var layer in Layers)
        {
            var target = layer.GetConfigurationTarget(/* phase */);
            foreach (var feature in Features)
            {
                feature.Configure(target);
            }
        }
        /*
            }
        }
        */
    }
}
