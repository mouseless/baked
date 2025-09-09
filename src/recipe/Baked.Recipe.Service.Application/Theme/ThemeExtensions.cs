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

    // TODO ActionModelAttribute is set at int.MaxValue - 10, will set a max for API and min for UI
    const int ORDER_UI_DEFAULT_VALUE = int.MaxValue - 5;

    #region Descriptor Builder & Schema

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

    public static void AddTypeSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<TSchema> schema,
        Func<TypeModelMetadataContext, bool>? whenType = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddTypeSchema(
        schema: _ => schema(),
        whenType: whenType,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddTypeSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<TypeModelMetadataContext, TSchema> schema,
        Func<TypeModelMetadataContext, bool>? whenType = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddTypeSchema(
        schema: (c, _) => schema(c),
        whenType: whenType,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddTypeSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<TypeModelMetadataContext, ComponentContext, TSchema> schema,
        Func<TypeModelMetadataContext, bool>? whenType = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    )
    {
        whenType ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddTypeMetadata(
            attribute: c => new DescriptorBuilderAttribute<TSchema>()
            {
                Builder = cc => schema(c, cc),
                Filter = whenComponent
            },
            when: whenType,
            order: order.Value
        );
    }

    public static void AddPropertySchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<TSchema> schema,
        Func<PropertyModelContext, bool>? whenProperty = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddPropertySchema(
        schema: _ => schema(),
        whenProperty: whenProperty,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddPropertySchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<PropertyModelContext, TSchema> schema,
        Func<PropertyModelContext, bool>? whenProperty = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddPropertySchema(
        schema: (c, _) => schema(c),
        whenProperty: whenProperty,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddPropertySchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<PropertyModelContext, ComponentContext, TSchema> schema,
        Func<PropertyModelContext, bool>? whenProperty = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    )
    {
        whenProperty ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddPropertyMetadata(
            attribute: c => new DescriptorBuilderAttribute<TSchema>()
            {
                Builder = cc => schema(c, cc),
                Filter = whenComponent
            },
            when: whenProperty,
            order: order.Value
        );
    }

    public static void AddMethodSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<TSchema> schema,
        Func<MethodModelContext, bool>? whenMethod = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddMethodSchema(
        schema: _ => schema(),
        whenMethod: whenMethod,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddMethodSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<MethodModelContext, TSchema> schema,
        Func<MethodModelContext, bool>? whenMethod = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddMethodSchema(
        schema: (c, _) => schema(c),
        whenMethod: whenMethod,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddMethodSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<MethodModelContext, ComponentContext, TSchema> schema,
        Func<MethodModelContext, bool>? whenMethod = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    )
    {
        whenMethod ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddMethodMetadata(
            attribute: c => new DescriptorBuilderAttribute<TSchema>()
            {
                Builder = cc => schema(c, cc),
                Filter = whenComponent
            },
            when: c => c.Type.Has<ControllerModelAttribute>() && c.Method.Has<ActionModelAttribute>() && whenMethod(c),
            order: order.Value
        );
    }

    public static void AddParameterSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<TSchema> schema,
        Func<ParameterModelContext, bool>? whenParameter = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddParameterSchema(
        schema: _ => schema(),
        whenParameter: whenParameter,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddParameterSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ParameterModelContext, TSchema> schema,
        Func<ParameterModelContext, bool>? whenParameter = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) => conventions.AddParameterSchema(
        schema: (c, _) => schema(c),
        whenParameter: whenParameter,
        whenComponent: whenComponent,
        order: order
    );

    public static void AddParameterSchema<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ParameterModelContext, ComponentContext, TSchema> schema,
        Func<ParameterModelContext, bool>? whenParameter = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    )
    {
        whenParameter ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddParameterMetadata(
            attribute: c => new DescriptorBuilderAttribute<TSchema>()
            {
                Builder = cc => schema(c, cc),
                Filter = whenComponent
            },
            when: c => c.Type.Has<ControllerModelAttribute>() && c.Parameter.Has<ParameterModelAttribute>() && whenParameter(c),
            order: order.Value
        );
    }

    #endregion

    #region Component Descriptor Builder & Component

    public static IComponentDescriptor GetComponent(this ICustomAttributesModel metadata, ComponentContext context)
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

    public static void AddTypeComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ComponentDescriptor<TSchema>> component,
        Func<TypeModelMetadataContext, bool>? whenType = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema =>
        conventions.AddTypeComponent(
            component: _ => component(),
            whenType: whenType,
            whenComponent: whenComponent,
            order: order
        );

    public static void AddTypeComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<TypeModelMetadataContext, ComponentDescriptor<TSchema>> component,
        Func<TypeModelMetadataContext, bool>? whenType = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema =>
        conventions.AddTypeComponent(
            component: (c, _) => component(c),
            whenType: whenType,
            whenComponent: whenComponent,
            order: order
        );

    public static void AddTypeComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<TypeModelMetadataContext, ComponentContext, ComponentDescriptor<TSchema>> component,
        Func<TypeModelMetadataContext, bool>? whenType = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema
    {
        whenType ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddTypeMetadata(
            apply: (c, add) =>
            {
                add(c.Type, new ComponentDescriptorBuilderAttribute<TSchema>()
                {
                    Builder = cc => component(c, cc),
                    Filter = whenComponent
                });
                add(c.Type, new ContextBasedSchemaAttribute(typeof(TSchema))
                {
                    Filter = whenComponent
                });
            },
            when: c => whenType(c),
            order: order.Value
        );
    }

    public static void AddPropertyComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ComponentDescriptor<TSchema>> component,
        Func<PropertyModelContext, bool>? whenProperty = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema =>
        conventions.AddPropertyComponent(
            component: _ => component(),
            whenProperty: whenProperty,
            whenComponent: whenComponent,
            order: order
        );

    public static void AddPropertyComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<PropertyModelContext, ComponentDescriptor<TSchema>> component,
        Func<PropertyModelContext, bool>? whenProperty = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema =>
        conventions.AddPropertyComponent(
            component: (c, _) => component(c),
            whenProperty: whenProperty,
            whenComponent: whenComponent,
            order: order
        );

    public static void AddPropertyComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<PropertyModelContext, ComponentContext, ComponentDescriptor<TSchema>> component,
        Func<PropertyModelContext, bool>? whenProperty = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema
    {
        whenProperty ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddPropertyMetadata(
            apply: (c, add) =>
            {
                add(c.Property, new ComponentDescriptorBuilderAttribute<TSchema>()
                {
                    Builder = cc => component(c, cc),
                    Filter = whenComponent
                });
                add(c.Property, new ContextBasedSchemaAttribute(typeof(TSchema))
                {
                    Filter = whenComponent
                });
            },
            when: c => whenProperty(c),
            order: order.Value
        );
    }

    public static void AddMethodComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ComponentDescriptor<TSchema>> component,
        Func<MethodModelContext, bool>? whenMethod = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema =>
        conventions.AddMethodComponent(
            component: _ => component(),
            whenMethod: whenMethod,
            whenComponent: whenComponent,
            order: order
        );

    public static void AddMethodComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<MethodModelContext, ComponentDescriptor<TSchema>> component,
        Func<MethodModelContext, bool>? whenMethod = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema =>
        conventions.AddMethodComponent(
            component: (c, _) => component(c),
            whenMethod: whenMethod,
            whenComponent: whenComponent,
            order: order
        );

    public static void AddMethodComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<MethodModelContext, ComponentContext, ComponentDescriptor<TSchema>> component,
        Func<MethodModelContext, bool>? whenMethod = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema
    {
        whenMethod ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddMethodMetadata(
            apply: (c, add) =>
            {
                add(c.Method, new ComponentDescriptorBuilderAttribute<TSchema>()
                {
                    Builder = cc => component(c, cc),
                    Filter = whenComponent
                });
                add(c.Method, new ContextBasedSchemaAttribute(typeof(TSchema))
                {
                    Filter = whenComponent
                });
            },
            when: c => whenMethod(c),
            order: order.Value
        );
    }

    public static void AddParameterComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ComponentDescriptor<TSchema>> component,
        Func<ParameterModelContext, bool>? whenParameter = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema =>
        conventions.AddParameterComponent(
            component: _ => component(),
            whenParameter: whenParameter,
            whenComponent: whenComponent,
            order: order
        );

    public static void AddParameterComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ParameterModelContext, ComponentDescriptor<TSchema>> component,
        Func<ParameterModelContext, bool>? whenParameter = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema =>
        conventions.AddParameterComponent(
            component: (c, _) => component(c),
            whenParameter: whenParameter,
            whenComponent: whenComponent,
            order: order
        );

    public static void AddParameterComponent<TSchema>(this IDomainModelConventionCollection conventions,
        Func<ParameterModelContext, ComponentContext, ComponentDescriptor<TSchema>> component,
        Func<ParameterModelContext, bool>? whenParameter = default,
        Func<ComponentContext, bool>? whenComponent = default,
        int? order = default
    ) where TSchema : IComponentSchema
    {
        whenParameter ??= c => true;
        whenComponent ??= c => true;
        order ??= ORDER_UI_DEFAULT_VALUE;

        conventions.AddParameterMetadata(
            apply: (c, add) =>
            {
                add(c.Parameter, new ComponentDescriptorBuilderAttribute<TSchema>()
                {
                    Builder = cc => component(c, cc),
                    Filter = whenComponent
                });
                add(c.Parameter, new ContextBasedSchemaAttribute(typeof(TSchema))
                {
                    Filter = whenComponent
                });
            },
            when: c => whenParameter(c),
            order: order.Value
        );
    }

    #endregion
}