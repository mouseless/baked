using Domain.Business;
using DomainModelOverReflection.Models;
using System.Reflection;

namespace DomainModelOverReflection.Api;

public record ControllerModel(string Name, List<ActionModel> Actions)
{
    public ControllerModel(Type type)
        : this(type.Name, new())
    {
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        bool isQuery = fields.Any(f => f.IsPrivate && f.FieldType.Name == typeof(IQueryContext<>).Name);

        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? Array.Empty<MethodInfo>();

        foreach (var method in methods)
        {
            if (!method.IsConstructor && !method.IsSpecialName && !method.CustomAttributes.Any())
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

    public ControllerModel(TypeModel typeModel)
        : this(typeModel.Name, new())
    {
        bool isQuery = typeModel.Fields.Any(f => f.IsPrivate && f.Type.Name == typeof(IQueryContext<>).Name);

        foreach (var method in typeModel.Methods)
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
