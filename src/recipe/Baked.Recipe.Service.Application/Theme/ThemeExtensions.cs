using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Theme;
using Baked.Ui;

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

    public static IEnumerable<T> WhereAppliesTo<T>(this IEnumerable<T> enumerable, ComponentContext context) =>
        enumerable.Where(c => c is not IComponentContextFilter when || when.AppliesTo(context));

    public static IComponentDescriptor GetComponentDescriptor(this ICustomAttributesModel metadata, ComponentContext context)
    {
        if (!metadata.TryGetAll<ContextBasedSchemaAttribute>(out var contextBasedComponents)) { throw new($"{metadata} has no compatible component descriptor for {context.Path}"); }

        var contextBasedComponent = contextBasedComponents.WhereAppliesTo(context).LastOrDefault() ??
            throw new($"{metadata} has no compatible component descriptor for {context.Path}");

        var builderType = typeof(ComponentDescriptorBuilderAttribute<>).MakeGenericType(contextBasedComponent.SchemaType);
        if (!metadata.TryGetAll(builderType, out var builders))
        {
            throw new($"{metadata} is expected to have a component descriptor builder of type {contextBasedComponent.SchemaType}");
        }

        return builders
            .Cast<IComponentContextBasedBuilder<IComponentDescriptor>>()
            .WhereAppliesTo(context)
            .LastOrDefault()
            ?.Build(context) ??
            throw new($"{metadata} is expected to have a component descriptor builder of type {contextBasedComponent.SchemaType}");
    }

    public static List<TSchema> GetSchemas<TSchema>(this ICustomAttributesModel metadata, ComponentContext context)
    {
        var result = new List<TSchema>();

        if (metadata.TryGetAll<DescriptorBuilderAttribute<TSchema>>(out var builders))
        {
            result.AddRange(builders
                .WhereAppliesTo(context)
                .Cast<IComponentContextBasedBuilder<TSchema>>()
                .Select(b => b.Build(context))
            );
        }

        return result;
    }

    public static TSchema? GetSchema<TSchema>(this ICustomAttributesModel metadata, ComponentContext context)
    {
        if (!metadata.TryGetAll<DescriptorBuilderAttribute<TSchema>>(out var builders)) { return default; }

        var builder = builders
            .WhereAppliesTo(context)
            .Cast<IComponentContextBasedBuilder<TSchema>>()
            .LastOrDefault();
        if (builder is null) { return default; }

        return builder.Build(context);
    }

    // TODO ActionModelAttribute is set at int.MaxValue - 10, will set a max for API and min for UI
    const int ORDER_UI_DEFAULT_VALUE = int.MaxValue - 5;

    public static void AddTypeDescriptor<TSchemaPart>(this IDomainModelConventionCollection conventions,
        Func<TypeModelMetadataContext, TSchemaPart> schemaPart,
        Func<TypeModelMetadataContext, bool>? whenType = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddTypeDescriptor(
        schemaPart: (c, _) => schemaPart(c),
        whenType: whenType,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddTypeDescriptor<TSchemaPart>(this IDomainModelConventionCollection conventions,
        Func<TypeModelMetadataContext, ComponentContext, TSchemaPart> schemaPart,
        Func<TypeModelMetadataContext, bool>? whenType = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    )
    {
        whenType ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddTypeMetadata(
            attribute: c => new DescriptorBuilderAttribute<TSchemaPart>()
            {
                Builder = cc => schemaPart(c, cc),
                Filter = whenComponent
            },
            when: whenType,
            order: order.Value
        );
    }

    public static void AddPropertyDescriptor<TSchemaPart>(this IDomainModelConventionCollection conventions,
        Func<PropertyModelContext, TSchemaPart> schemaPart,
        Func<PropertyModelContext, bool>? whenProperty = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddPropertyDescriptor(
        schemaPart: (c, _) => schemaPart(c),
        whenProperty: whenProperty,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddPropertyDescriptor<TSchemaPart>(this IDomainModelConventionCollection conventions,
        Func<PropertyModelContext, ComponentContext, TSchemaPart> schemaPart,
        Func<PropertyModelContext, bool>? whenProperty = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    )
    {
        whenProperty ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddPropertyMetadata(
            attribute: c => new DescriptorBuilderAttribute<TSchemaPart>()
            {
                Builder = cc => schemaPart(c, cc),
                Filter = whenComponent
            },
            when: whenProperty,
            order: order.Value
        );
    }

    public static void AddMethodDescriptor<TSchemaPart>(this IDomainModelConventionCollection conventions,
        Func<MethodModelContext, TSchemaPart> schemaPart,
        Func<MethodModelContext, bool>? whenMethod = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddMethodDescriptor(
        schemaPart: (c, _) => schemaPart(c),
        whenMethod: whenMethod,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddMethodDescriptor<TSchemaPart>(this IDomainModelConventionCollection conventions,
        Func<MethodModelContext, ComponentContext, TSchemaPart> schemaPart,
        Func<MethodModelContext, bool>? whenMethod = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    )
    {
        whenMethod ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddMethodMetadata(
            attribute: c => new DescriptorBuilderAttribute<TSchemaPart>()
            {
                Builder = cc => schemaPart(c, cc),
                Filter = whenComponent
            },
            when: c => c.Type.Has<ControllerModelAttribute>() && c.Method.Has<ActionModelAttribute>() && whenMethod(c),
            order: order.Value
        );
    }

    public static void AddParameterDescriptor<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ParameterModelContext, TSchema> schema,
        Func<ParameterModelContext, bool>? whenParameter = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddParameterDescriptor(
        schemaPart: (c, _) => schema(c),
        whenParameter: whenParameter,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddParameterDescriptor<TSchemaPart>(this IDomainModelConventionCollection conventions,
        Func<ParameterModelContext, ComponentContext, TSchemaPart> schemaPart,
        Func<ParameterModelContext, bool>? whenParameter = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    )
    {
        whenParameter ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddParameterMetadata(
            attribute: c => new DescriptorBuilderAttribute<TSchemaPart>()
            {
                Builder = cc => schemaPart(c, cc),
                Filter = whenComponent
            },
            when: c => c.Type.Has<ControllerModelAttribute>() && c.Parameter.Has<ParameterModelAttribute>() && whenParameter(c),
            order: order.Value
        );
    }
}