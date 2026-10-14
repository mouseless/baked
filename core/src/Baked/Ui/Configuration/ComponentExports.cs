using System.Reflection;

namespace Baked.Ui.Configuration;

public class ComponentExports : List<string>
{
    public void AddFromAssembly(Type theme) =>
        AddFromAssembly(theme.Assembly);

    public void AddFromAssembly(Assembly assembly)
    {
        var componentTypes = assembly.GetTypes()
            .Where(t =>
                t.IsAssignableTo(typeof(IComponentSchema)) &&
                !t.IsInterface &&
                !t.IsAbstract
            )
            .Select(t => t.Name);

        AddRange(componentTypes);
    }
}