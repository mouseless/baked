using Humanizer;

namespace Baked.Ui.Configuration;

public abstract record PluginBase : IPlugin
{
    public virtual string Name => GetType().Name.Replace("Plugin", string.Empty).Camelize();
}