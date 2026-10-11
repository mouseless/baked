namespace Baked.Ui;

public static class Evaluate
{
    public static Composables Composable { get; } = new();

    public static IsConstraint Is(object? value,
        Action<IsConstraint>? options = default
    ) => options.Apply(new() { Is = value });

    public static IsNotConstraint IsNot(object? value,
        Action<IsNotConstraint>? options = default
    ) => options.Apply(new() { IsNot = value });

    public class Composables
    {
        public ComposableConstraint Use(string composable,
            Action<ComposableConstraint>? options = default
        ) => options.Apply(new() { Composable = composable.StartsWith("use") ? composable : $"use{composable}" });
    }
}