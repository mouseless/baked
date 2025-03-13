using Baked.Architecture;
using Baked.Domain.Model;
using Baked.Theme;
using Baked.Ui;

namespace Baked;

public static class ThemeExtensions
{
    public static void AddTheme(this List<IFeature> features, Func<ThemeConfigurator, IFeature<ThemeConfigurator>> configure) =>
        features.Add(configure(new()));

    public static void AddPages<TSchema>(this PageDescriptors pages, ModelCollection<TypeModel> types)
        where TSchema : IComponentSchema
    {
        foreach (var type in types.Having<ComponentDescriptorAttribute<TSchema>>())
        {
            var component = type.GetMetadata().GetSingle<ComponentDescriptorAttribute<TSchema>>();

            pages.Add(component);
        }
    }
}