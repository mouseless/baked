namespace Baked.Playground.Business;

public record Record(string Text, int Numeric)
{
    internal string Internal => Text;
}