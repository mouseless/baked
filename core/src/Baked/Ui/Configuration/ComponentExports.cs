using Baked.Architecture;
using System.Reflection;

namespace Baked.Ui.Configuration;

public class ComponentExports : List<string>
{
    public void AddFromThemeAssembly<TTheme>(TTheme _) where TTheme : IFeature<ThemeFeature.Configurator> =>
        AddFromAssembly(typeof(TTheme).Assembly);

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