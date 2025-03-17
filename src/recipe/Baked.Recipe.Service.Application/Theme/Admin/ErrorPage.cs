using Baked.Ui;

namespace Baked.Theme.Admin;

public record ErrorPage(string FooterInfo, string SafeLinksMessage) : IComponentSchema
{
    public Dictionary<int, Info> ErrorInfos { get; init; } = [];
    public string FooterInfo { get; set; } = FooterInfo;
    public List<IComponentDescriptor> SafeLinks { get; init; } = [];
    public string SafeLinksMessage { get; set; } = SafeLinksMessage;

    public record Info(string Title, string Message);
}