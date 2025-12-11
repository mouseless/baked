namespace Baked.Ui;

public static class Constraints
{
    public static Composables Composable { get; } = new();

    public static IsConstraint Is(string value,
        Action<IsConstraint>? options = default
    ) => new(value);

    public static IsNotConstraint IsNot(string value,
        Action<IsNotConstraint>? options = default
    ) => new(value);

    public class Composables
    {
        public ComposableConstraint Use(string composable,
            Action<ComposableConstraint>? options = default
        )
        {
            composable = composable.StartsWith("use") ? composable : $"use{composable}";
            var result = new ComposableConstraint(composable);

            options?.Invoke(result);

            return result;
        }
    }
}