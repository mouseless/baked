namespace Baked.Ui;

public record ErrorPage : IComponentSchema
{
    public Dictionary<int, Info> ErrorInfos { get; init; } = [];
    public string? FooterInfo { get; set; }
    public List<IComponentDescriptor> SafeLinks { get; init; } = [];
    public string? SafeLinksMessage { get; set; }

    public record Info
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool? ShowSafeLinks { get; set; }
        public bool? CustomMessage { get; set; }
    }
}