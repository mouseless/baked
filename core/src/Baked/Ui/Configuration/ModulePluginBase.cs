namespace Baked.Ui.Configuration;

public abstract record ModulePluginBase(
    string BasePath = "./runtime/plugins/",
    PluginResolver Resolver = PluginResolver.MetaUrl
) : PluginBase(BasePath, Resolver);