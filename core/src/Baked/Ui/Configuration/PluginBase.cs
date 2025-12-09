using Humanizer;

namespace Baked.Ui.Configuration;

public abstract record PluginBase(bool Module = false) : IPlugin
{
    public bool Module { get; } = Module;
    public virtual string Name => GetType().Name.Replace("Plugin", string.Empty).Camelize();
}