namespace Do.Architecture;

public class LayerConfigurator
{
    public static LayerConfigurator Create<TTarget>(TTarget target) => new(typeof(TTarget), target);
    public static LayerConfigurator Create<TTarget1, TTarget2>(TTarget1 target1, TTarget2 target2) => new(typeof((TTarget1, TTarget2)), (target1, target2));
    public static LayerConfigurator Create<TTarget1, TTarget2, TTarget3>(TTarget1 target1, TTarget2 target2, TTarget3 target3) => new(typeof((TTarget1, TTarget2, TTarget3)), (target1, target2, target3));

    readonly Type _expectedType;
    readonly object? _target;

    LayerConfigurator(Type expectedType, object? target)
    {
        _expectedType = expectedType;
        _target = target;
    }

    public void Configure<TTarget>(Action<TTarget> configuration)
    {
        if (_target is null) { return; }
        if (_expectedType != typeof(TTarget)) { return; }

        configuration((TTarget)_target);
    }

    public void Configure<TTarget1, TTarget2>(Action<TTarget1, TTarget2> configuration)
    {
        if (_target is null) { return; }
        if (_expectedType != typeof((TTarget1, TTarget2))) { return; }

        var (target1, target2) = ((TTarget1, TTarget2))_target;

        configuration(target1, target2);
    }

    public void Configure<TTarget1, TTarget2, TTarget3>(Action<TTarget1, TTarget2, TTarget3> configuration)
    {
        if (_target is null) { return; }
        if (_expectedType != typeof((TTarget1, TTarget2, TTarget3))) { return; }

        var (target1, target2, target3) = ((TTarget1, TTarget2, TTarget3))_target;

        configuration(target1, target2, target3);
    }

    public override bool Equals(object? obj) =>
        obj is LayerConfigurator configurator &&
        EqualityComparer<Type>.Default.Equals(_expectedType, configurator._expectedType) &&
        EqualityComparer<object?>.Default.Equals(_target, configurator._target);

    public override int GetHashCode() => HashCode.Combine(_expectedType, _target);
}
