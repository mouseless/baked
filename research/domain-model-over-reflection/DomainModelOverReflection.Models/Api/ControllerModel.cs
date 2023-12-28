using DomainModelOverReflection.Models.Domain;
using System.Reflection;

namespace DomainModelOverReflection.Api;

public record ControllerModel(string Name, List<ActionModel> Actions)
{
    public ControllerModel(Type type)
        : this(type.Name, new())
    {
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        bool isQuery = fields.Any(f => f.IsPrivate && f.FieldType.Name.Contains("IQueryContext"));

        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? Array.Empty<MethodInfo>();

        foreach (var method in methods.Where(m => !m.IsConstructor && !m.IsSpecialName && m.Name != "With"))
        {
            if (isQuery)
            {
                Actions.Add(new(method, HttpMethod.Get));
            }
            else
            {
                Actions.Add(new(method));
            }
        }
    }

    public ControllerModel(TypeModel typeModel)
        : this(typeModel.Name, new())
    {
        bool isQuery = typeModel.Fields.Any(f => f.IsPrivate && f.Type.Contains("IQueryContext"));

        foreach (var method in typeModel.Methods.Where(m => m.IsPublic && m.Name != "With"))
        {
            if (isQuery)
            {
                Actions.Add(new(method, HttpMethod.Get));
            }
            else
            {
                Actions.Add(new(method));
            }
        }
    }
}
