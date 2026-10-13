namespace Baked.Ui;

public record Button : IComponentSchema
{
    public string? Icon { get; set; }
    public string? Label { get; set; }
    public string? Variant { get; set; }
    public string? Severity { get; set; }
    public bool? Rounded { get; set; }
    public string? Size { get; set; }
    public Overlay? OverlayOptions { get; set; }

    public record Overlay
    {
        public string Title { get; set; } = string.Empty;
        public IComponentDescriptor Form { get; set; } = MissingComponent.Empty;
        public OverlayMode? Mode { get; set; }
        public bool? DismissableMask { get; set; }
        public Button Cancel { get; set; } = new();
    }

    public enum OverlayMode
    {
        Popover,
        Dialog
    }
}