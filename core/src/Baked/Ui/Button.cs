namespace Baked.Ui;

public record Button : IComponentSchema
{
    public string? Icon { get; set; }
    public string? Label { get; set; }
    public string? Variant { get; set; }
    public string? Severity { get; set; }
    public bool? Rounded { get; set; }
    public string? Size { get; set; }
    public Dialog? DialogOptions { get; set; }

    public record Dialog
    {
        public string Title { get; set; } = string.Empty;
        public DialogMode? Mode { get; set; }
        public bool? DismissableMask { get; set; }
        public IComponentDescriptor? Content { get; set; }
        public IComponentDescriptor? Form { get; set; }
        public Button? Submit { get; set; }
        public Button Cancel { get; set; } = new();
    }

    public enum DialogMode
    {
        Popover,
        Modal
    }
}