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

    public void Configure<T1, T2>(Action<T1, T2> configuration)
    {
        if (_target is null) { return; }
        if (_expectedType != typeof((T1, T2))) { return; }

        var (t1, t2) = ((T1, T2))_target;

        configuration(t1, t2);
    }

    public void Configure<T1, T2, T3>(Action<T1, T2, T3> configuration)
    {
        if (_target is null) { return; }
        if (_expectedType != typeof((T1, T2, T3))) { return; }

        var (t1, t2, t3) = ((T1, T2, T3))_target;

        configuration(t1, t2, t3);
    }

    public override bool Equals(object? obj) =>
        obj is ConfigurationTarget target &&
       EqualityComparer<Type>.Default.Equals(_expectedType, target._expectedType) &&
       EqualityComparer<object?>.Default.Equals(_target, target._target);

    public override int GetHashCode() => HashCode.Combine(_expectedType, _target);
}
