namespace Baked.Test.Business;

public class ClassWithMultipleGenericInterfaces : IEquatable<int>, IEquatable<string>
{
    public int Numeric { get; set; } = default!;

    bool IEquatable<int>.Equals(int other) =>
        Numeric.Equals(other);

    bool IEquatable<string>.Equals(string? other) =>
        $"{Numeric}".Equals(other);
}