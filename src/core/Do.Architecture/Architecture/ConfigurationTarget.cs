namespace Do.Architecture;

public class ConfigurationTarget
{
    public static readonly ConfigurationTarget Empty = new(typeof(object), null);
    public static ConfigurationTarget Create<T>(T target) => new(typeof(T), target);

    readonly Type _expectedType;
    readonly object? _target;

    ConfigurationTarget(Type expectedType, object? target)
    {
        _expectedType = expectedType;
        _target = target;
    }

    public void Configure<T>(Action<T> configuration)
    {
        if (_target is null) { return; }
        if (_expectedType != typeof(T)) { return; }

        configuration((T)_target);
    }

    public override bool Equals(object? obj) =>
        obj is ConfigurationTarget target &&
       EqualityComparer<Type>.Default.Equals(_expectedType, target._expectedType) &&
       EqualityComparer<object?>.Default.Equals(_target, target._target);

    public override int GetHashCode() => HashCode.Combine(_expectedType, _target);
}
