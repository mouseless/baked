namespace Baked.Ui.Configuration;

public record AppDescriptor
{
    public List<IPlugin> Plugins { get; } = [];
    public IComponentDescriptor? Error { get; set; }
    public I18nDescriptor I18n { get; set; } = new();
    public List<RoutePattern> RoutePatterns { get; init; } = [];

    public record RoutePattern(string Name, string Value);
}