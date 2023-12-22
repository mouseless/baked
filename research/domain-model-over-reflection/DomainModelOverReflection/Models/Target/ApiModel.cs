using System.Reflection;

namespace DomainModelOverReflection.Models.Target;

public class ApiModel
{
    public static ApiModel Build(Assembly? assembly)
    {
        var model = new ApiModel();

        var types = assembly?.GetTypes() ?? Array.Empty<Type>();

        foreach (var type in types)
        {
            if (type.Namespace == "DomainModelOverReflection.Business")
            {
                if (type.GetConstructors().Any(c => c.IsPublic && c.GetParameters().Any()))
                {
                    model.ControllerModels.Add(new(type));
                }
                else
                {
                    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                    if (methods.Any(m => m.Name == "With") && methods.Any(m => m.Name == "Process"))
                    {
                        model.ControllerModels.Add(new(type));
                    }
                }
            }
        }

        return model;
    }

    public List<ControllerModel> ControllerModels { get; } = new();
}
