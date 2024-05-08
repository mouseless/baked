namespace Do.Test.Business;

public class ClassWithMultipleGenericInterfaces : IEquatable<int>, IEquatable<string>
{
    public int Numeric { get; set; } = default!;

    public bool Equals(int other) =>
        Numeric.Equals(other);

    public bool Equals(string? other) =>
        $"{Numeric}".Equals(other);
}
