namespace Baked.Playground.Business;

public struct Struct(string text, int numeric)
{
    public string Text { get; } = text;
    public int Numeric { get; } = numeric;
}