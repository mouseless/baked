namespace Baked.Ui;

public record Label
{
    public string? Text { get; set; }
    public string? Mode { get; set; }
    public string? Variant { get; set; }
    public bool? ShowOptionality { get; set; }

    public void FloatIn(Func<string> defaultText) =>
        SetMode("float", Text ?? defaultText(), variant: "in");

    public void FloatOn(Func<string> defaultText) =>
        SetMode("float", Text ?? defaultText(), variant: "on");

    public void FLoatOver(Func<string> defaultText) =>
        SetMode("float", Text ?? defaultText(), variant: "over");

    public void Ifta(Func<string> defaultText) =>
        SetMode("ifta", Text ?? defaultText());

    public void None() =>
        SetMode(null, null);

    void SetMode(string? mode, string? label,
        string? variant = default
    )
    {
        Mode = mode;
        Text = label;
        Variant = variant;
    }
}