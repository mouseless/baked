namespace Baked.Ui.Configuration;

public interface IPlugin
{
    string BasePath { get; }
    string Name { get; }
    PluginResolver Resolver { get; }
}