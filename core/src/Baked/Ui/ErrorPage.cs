namespace Baked.Ui;

public record ErrorPage : IComponentSchema
{
    public Dictionary<int, Info> ErrorInfos { get; init; } = [];
    public string FooterInfo { get; set; } = "If you cannot reach the page you want please contact the system administrator";
    public List<IComponentDescriptor> SafeLinks { get; init; } = [];
    public string SafeLinksMessage { get; set; } = "Try the links from the menu below to view the page you want to access:";

    public record Info(string Title, string Message)
    {
        public bool? ShowSafeLinks { get; set; }
        public bool? CustomMessage { get; set; }
    }
}