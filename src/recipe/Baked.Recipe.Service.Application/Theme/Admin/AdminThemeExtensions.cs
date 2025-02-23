using Baked.Domain.Model;
using Baked.Theme;
using Baked.Theme.Admin;
using Baked.Ui;
using Humanizer;

namespace Baked;

public static class AdminThemeExtensions
{
    public static AdminThemeFeature Admin(this ThemeConfigurator _,
        Func<string, string>? defaultPageName = default
    ) => new(defaultPageName ?? (s => s.Kebaberize()));

    public static void AddPages<TSchema>(this PageDescriptors pages, ModelCollection<TypeModel> types, Func<string, string> defaultPageName)
        where TSchema : IComponentSchema
    {
        foreach (var type in types.Having<ComponentDescriptorAttribute<TSchema>>())
        {
            var component = type.GetMetadata().GetSingle<ComponentDescriptorAttribute<TSchema>>();

            pages.Add(component.Name ?? defaultPageName(type.Name), component);
        }
    }
}