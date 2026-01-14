namespace Baked.Orm;

public struct Id(string _value)
{
    public static Id Parse(object value) =>
        new($"{value}");

    public readonly string Value => _value;
}
