namespace Baked.Ui;

public static class Constraints
{
    public static Composables Composable { get; } = new();

    public static IsConstraint Is(string value,
        Action<IsConstraint>? options = default
    ) => options.Apply(new(value));

    public static IsNotConstraint IsNot(string value,
        Action<IsNotConstraint>? options = default
    ) => options.Apply(new(value));

    public class Composables
    {
        public ComposableConstraint Use(string composable,
            Action<ComposableConstraint>? options = default
        ) => options.Apply(new(composable.StartsWith("use") ? composable : $"use{composable}"));
    }
}