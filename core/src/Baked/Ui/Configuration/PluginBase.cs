using Humanizer;

namespace Baked.Ui.Configuration;

public abstract record PluginBase(
    string BasePath = "./app/plugins/",
    PluginResolver Resolver = PluginResolver.RootDir
) : IPlugin
{
    public string BasePath { get; } = BasePath;
    public virtual string Name => GetType().Name.Replace("Plugin", string.Empty).Camelize();
    public PluginResolver Resolver { get; } = Resolver;
}