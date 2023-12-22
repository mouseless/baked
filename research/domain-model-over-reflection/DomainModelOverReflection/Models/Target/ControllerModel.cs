using System.Reflection;

namespace DomainModelOverReflection.Models.Target;

public record ControllerModel(string Name, List<ActionModel> Actions)
{
    public ControllerModel(Type type)
        : this(type.Name, new())
    {
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        bool isQuery = fields.Any(f => f.IsPrivate && f.FieldType.IsAssignableTo(typeof(IQueryContext<>)));

        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? Array.Empty<MethodInfo>();

        foreach (var method in methods)
        {
            if (!method.IsConstructor && !method.IsSpecialName && !method.CustomAttributes.Any())
            {
                if (isQuery)
                {
                    Actions.Add(new(method, HttpMethod.Get));
                }
            }
        }
    }
}
