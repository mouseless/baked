using System.Reflection;

namespace DomainModelOverReflection.Models.Target;

public record ControllerModel(string Name, List<ActionModel> Actions)
{
    public ControllerModel(string name, List<MethodInfo> methodInfos)
        : this(name, new List<ActionModel>())
    {
        foreach (var method in methodInfos)
        {
            Actions.Add(new(method));
        }
    }
}
