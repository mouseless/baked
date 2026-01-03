namespace Baked.Playground.Business;

public class Data(string text, int numeric)
{
    public int Numeric { get; } = numeric;
    public string Text { get; } = text;
}