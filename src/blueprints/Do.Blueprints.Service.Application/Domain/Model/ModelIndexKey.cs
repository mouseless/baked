namespace Do.Domain.Model;

public class ModelIndexKey(object key) : IEquatable<ModelIndexKey>
{
    readonly object _key = key;

    public bool Equals(ModelIndexKey? other) =>
        other is not null && other.GetHashCode() == _key.GetHashCode();

    public override bool Equals(object? obj) =>
        Equals(obj);

    public override int GetHashCode() =>
        _key.GetHashCode();
}
