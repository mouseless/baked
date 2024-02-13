namespace Do.Communication;

public record Response(
    string Content
)
{
    public bool HasContent => !string.IsNullOrWhiteSpace(Content);
}
