using Baked.Architecture;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Theme;
using Baked.Ui;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class ThemeExtensions
{
    public static void AddTheme(this List<IFeature> features, Func<ThemeConfigurator, IFeature<ThemeConfigurator>> configure) =>
        features.Add(configure(new()));

    public static T Apply<T>(this Action<T>? action, T result)
    {
        action?.Invoke(result);

        return result;
    }

    public static Route Index(this Router router,
        string? title = default,
        string? icon = default
    ) => router.Root("/", title ?? "Home", icon ?? "pi pi-home");

    public static Route Root(this Router router, string path, string title, string icon) =>
        router.Create(path, title) with { Icon = icon, SideMenu = true, ErrorSafeLink = true };

    public static Route Child(this Router router, string path, string title, string parentPath) =>
        router.Create(path, title) with { ParentPath = parentPath };

    public static MethodModel GetMethod(this TypeModel type, string name) =>
        type.GetMembers().Methods[name];

    public static ActionModelAttribute GetAction(this MethodModel method) =>
        method.Get<ActionModelAttribute>();

    public static string GetRoute(this ActionModelAttribute action, List<(string key, string value)> routeParameters)
    {
        var routeParts = action.GetRoute().Split("/");
        foreach (var (key, value) in routeParameters)
        {
            var parameter = action.Parameter[key];
            if (parameter.FromRoute)
            {
                routeParts[parameter.RoutePosition] = value;
            }
        }

        return routeParts.Join('/');
    }

    public static IEnumerable<string> GetEnumNames(this TypeModel type) =>
        [.. type.SkipNullable().Apply(t => Enum.GetNames(t).Select(n => n.TrimStart('_')))];

    public static TypeModel SkipNullable(this TypeModel type)
    {
        if (!type.IsAssignableTo(typeof(Nullable<>))) { return type; }
        if (!type.TryGetGenerics(out var generics)) { throw new InvalidOperationException($"{type.Name} doesn't provide generics information"); }

        return generics.GenericTypeArguments.First().Model;
    }

    public static bool TryGet<TSchema>(this TypeModel type, [NotNullWhen(true)] out TSchema? schema)
        where TSchema : IComponentSchema
    {
        schema = default;

        if (!type.TryGetMembers(out var members)) { return false; }
        if (!members.TryGet<ComponentDescriptorAttribute<TSchema>>(out var descriptor)) { return false; }

        schema = descriptor.Schema;

        return true;
    }

    public static TSchema Get<TSchema>(this TypeModel type)
        where TSchema : IComponentSchema
    {
        if (!type.TryGet<TSchema>(out var result)) { throw new($"{type.Name} does not have ${typeof(TSchema).Name}"); }

        return result;
    }
}