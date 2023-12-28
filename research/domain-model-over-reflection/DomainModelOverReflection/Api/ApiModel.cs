using DomainModelOverReflection.Models;
using System.Reflection;

namespace DomainModelOverReflection.Api;

public class ApiModel
{
    public static ApiModel Build(Assembly? assembly)
    {
        var model = new ApiModel();

        var types = assembly?.GetTypes() ?? Array.Empty<Type>();

        foreach (var type in types)
        {
            if (type.Namespace == "Domain.Business")
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

    public static ApiModel Build(IDomainModel domainModel)
    {
        var model = new ApiModel();

        foreach (var type in domainModel.TypeModels)
        {
            //entity or query
            if (type.Constructors.Any(c => c.IsPublic && c.Parameters.Any()))
            {
                model.ControllerModels.Add(new(type));
            }
            else
            {
                // operation object
                if (type.Methods.Any(m => m.Name == "With") && type.Methods.Any(m => m.Name == "Process"))
                {
                    model.ControllerModels.Add(new(type));
                }
            }
        }

        return model;
    }

    public List<ControllerModel> ControllerModels { get; } = new();
}
