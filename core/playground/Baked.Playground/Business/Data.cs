namespace Baked.Playground.Business;

public class Data(string text, int numeric)
{
    public int Numeric { get; } = numeric;
    public string Text { get; } = text;

    public IPolymorphic Polymorphic => new ImplementedPolymorphic(Text);
    public string CalculatedText => $"Calculated {Text}";
}