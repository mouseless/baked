namespace Do.Test.Business;

public class ClassDTO(string text, int numeric)
{
    public int Numeric { get; } = numeric;
    public string Text { get; } = text;
}
