namespace Baked.Test.Ui;

public record Endpoint
{
    public string? Path { get; set; }
    public string Method { get; set; } = "POST";
}