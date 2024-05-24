namespace Do.Authorization;

public class RequireUserAttribute(
  string[]? claims = default
) : Attribute
{
    public bool Override { get; set; }

    public string[] Claims => claims ?? [];
}