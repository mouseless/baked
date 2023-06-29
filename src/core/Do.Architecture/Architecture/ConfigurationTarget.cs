namespace Do.Architecture;

public class ConfigurationTarget
{
    public static readonly ConfigurationTarget Empty = new(typeof(object), null);
    public static ConfigurationTarget Create<T>(T target) => new(typeof(T), target);

    private readonly Type _expectedType;
    private readonly object? _target;

    private ConfigurationTarget(Type expectedType, object? target)
    {
        _expectedType = expectedType;
        _target = target;
    }

    public void Apply<T>(Action<T> configuration)
    {
        if (_target is null) { return; }
        if (_expectedType != typeof(T)) { return; }

        configuration((T)_target);
    }
}
