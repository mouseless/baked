namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    OverloadModel[] Overloads,
    bool IsConstructor = false
) : IModel
{
    string IModel.Id { get; } = Name;
}

public static class MethodModelExtensions
{
    public static bool CanReturn(this MethodModel method, TypeModel type) =>
        method.Overloads.Any(o =>
            o.ReturnType == type ||
            (o.ReturnType?.IsAssignableTo<Task>() == true && o.ReturnType?.GenericTypeArguments.Any(a => a == type) == true)
        );
}
