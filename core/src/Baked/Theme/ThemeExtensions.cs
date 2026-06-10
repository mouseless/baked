using Baked.Architecture;
using Baked.Buildtime.Diagnostics;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Inspection;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Testing;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Baked.Ui.Configuration;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using static Baked.Ui.Datas;

namespace Baked;

public static class ThemeExtensions
{
    extension(List<IFeature> features)
    {
        public void AddTheme(FeatureFunc<ThemeConfigurator> configure) =>
            features.Add(configure(new()));
    }

    extension(DiagnosticCode)
    {
        public static DiagnosticCode MissingRequiredComponent => new(101, "missing-required-component");
        public static DiagnosticCode MissingRequiredComponentOfType => new(102, "missing-required-component-of-type");
        public static DiagnosticCode MissingRequiredSchema => new(103, "missing-required-schema");
        public static DiagnosticCode RequiresLocateAction => new(104, "requires-locate-action");
        public static DiagnosticCode MethodRequired => new(105, "method-required");
        public static DiagnosticCode MissingItem => new(106, "missing-item");
        public static DiagnosticCode InvalidState => new(107, "invalid-state");
    }

    extension<T>(Action<T>? action)
    {
        public T Apply(T result)
        {
            action?.Invoke(result);

            return result;
        }
    }

    extension(Router router)
    {
        public Route Index(
            string? title = default,
            string? icon = default
        ) => router.Root("/", title ?? "Home", icon ?? "pi pi-home");

        public Route Root<TType, TPage>(string path, string icon)
            where TPage : IPageSchema =>
            router.Root(path, icon) with { Page = p => p.Generated(g => g.Type<TType, TPage>()) };

        public Route Root<TType, TPage>(string path, string icon, string methodName)
            where TPage : IPageSchema =>
            router.Root(path, icon) with { Page = p => p.Generated(g => g.Method<TType, TPage>(methodName)) };

        public Route Root(string path, string icon) =>
            router.Root(path, $"{path}:Title", icon) with { Description = $"{path}:Description" };

        public Route Root(string path, string title, string icon) =>
            router.Create(path, title) with { Icon = icon, SideMenu = true, ErrorSafeLink = true };

        public Route RootDynamic<TType, TPage>(string path)
            where TPage : IPageSchema =>
            router.RootDynamic(path) with { Page = p => p.Generated(g => g.Type<TType, TPage>()) };

        public Route RootDynamic<TType, TPage>(string path, string methodName)
            where TPage : IPageSchema =>
            router.RootDynamic(path) with { Page = p => p.Generated(g => g.Method<TType, TPage>(methodName)) };

        public Route RootDynamic(string path) =>
            router.RootDynamic(path, $"{path}:Title") with { Description = $"{path}:Description" };

        public Route RootDynamic(string path, string title) =>
            router.Create(path, title) with { ErrorSafeLink = false, SideMenu = false };

        public Route Child<TType, TPage>(string path, string parentPath)
            where TPage : IPageSchema =>
            router.Child(path, parentPath) with { Page = p => p.Generated(g => g.Type<TType, TPage>()) };

        public Route Child<TType, TPage>(string path, string parentPath, string methodName)
            where TPage : IPageSchema =>
            router.Child(path, parentPath) with { Page = p => p.Generated(g => g.Method<TType, TPage>(methodName)) };

        public Route Child(string path, string parentPath) =>
            router.Child(path, $"{path}:Title", parentPath) with { Description = $"{path}:Description" };

        public Route Child(string path, string title, string parentPath) =>
            router.Create(path, title) with { ParentPath = parentPath };

        public Route ChildDynamic<TType, TPage>(string path, string parentPath)
            where TPage : IPageSchema =>
            router.ChildDynamic(path, parentPath) with { Page = p => p.Generated(g => g.Type<TType, TPage>()) };

        public Route ChildDynamic<TType, TPage>(string path, string parentPath, string methodName)
            where TPage : IPageSchema =>
            router.ChildDynamic(path, parentPath) with { Page = p => p.Generated(g => g.Method<TType, TPage>(methodName)) };

        public Route ChildDynamic(string path, string parentPath) =>
            router.ChildDynamic(path, $"{path}:Title", parentPath) with { Description = $"{path}:Description" };

        public Route ChildDynamic(string path, string title, string parentPath) =>
            router.Create(path, title) with { ParentPath = parentPath, ErrorSafeLink = false, SideMenu = false };
    }

    extension(Order order)
    {
        public Order Theme =>
            order.WithBase("Theme");

        internal Order ThemeDefault =>
            order.WithBase(order.Base ?? "Theme");
    }

    extension(IDomainModelConventionCollection conventions)
    {
        public void AddEntityRemoteData<TEntity>()
        {
            conventions.AddTypeSchema(
                when: c => c.Type.Is<TEntity>(),
                schema: (c, cc) =>
                {
                    if (!c.Type.GetControllerModel().Action.TryGetValue("Locate", out var locate))
                    {
                        throw DiagnosticCode.RequiresLocateAction.Exception(
                            $"`{c.Type.Name}` should have `Locate` action added"
                        );
                    }

                    return Remote(locate.GetRoute(), o => o.Params = Computed.UseRoute("params"));
                },
                order: Order.At.Override
            );
        }

        public void AddTypeSchema<TSchema>(Func<TSchema> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddTypeSchema(
            schema: _ => schema(),
            when: when,
            where: where,
            order: order
        );

        public void AddTypeSchema<TSchema>(Func<TypeModelMetadataContext, TSchema> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddTypeSchema(
            schema: (c, _) => schema(c),
            when: when,
            where: where,
            order: order
        );

        public void AddTypeSchema<TSchema>(Func<TypeModelMetadataContext, ComponentContext, TSchema> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        )
        {
            when ??= c => true;
            where ??= c => true;
            order = order.ThemeDefault.Add;

            conventions.AddTypeAttribute(
                attribute: c => new GeneratorAttribute<TSchema>
                {
                    Generator = cc => cc.Trace.CaptureDescriptor(c, cc, () => schema(c, cc), orderInfo: $"+{order}"),
                    Filter = where,
                    Trace = c.Trace
                },
                when: when,
                beforeBuildingIndexes: false,
                order: order
            );
        }

        public void RemoveTypeSchema<TSchema>(Func<TypeModelMetadataContext, bool> when,
            Order order = default
        )
        {
            conventions.RemoveTypeAttribute<GeneratorAttribute<TSchema>>(when: when,
                beforeBuildingIndexes: false,
                order: order.ThemeDefault.Add
            );
        }

        public void AddPropertySchema<TSchema>(Func<TSchema> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddPropertySchema(
            schema: _ => schema(),
            when: when,
            where: where,
            order: order
        );

        public void AddPropertySchema<TSchema>(Func<PropertyModelContext, TSchema> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddPropertySchema(
            schema: (c, _) => schema(c),
            when: when,
            where: where,
            order: order
        );

        public void AddPropertySchema<TSchema>(Func<PropertyModelContext, ComponentContext, TSchema> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        )
        {
            when ??= c => true;
            where ??= c => true;
            order = order.ThemeDefault.Add;

            conventions.AddPropertyAttribute(
                attribute: c => new GeneratorAttribute<TSchema>
                {
                    Generator = cc => cc.Trace.CaptureDescriptor(c, cc, () => schema(c, cc), orderInfo: $"+{order}"),
                    Filter = where,
                    Trace = c.Trace
                },
                when: when,
                beforeBuildingIndexes: false,
                order: order
            );
        }

        public void RemovePropertySchema<TSchema>(Func<PropertyModelContext, bool> when,
            Order order = default
        )
        {
            conventions.RemovePropertyAttribute<GeneratorAttribute<TSchema>>(when: when,
                beforeBuildingIndexes: false,
                order: order.ThemeDefault.Add
            );
        }

        public void AddMethodSchema<TSchema>(Func<TSchema> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddMethodSchema(
            schema: _ => schema(),
            when: when,
            where: where,
            order: order
        );

        public void AddMethodSchema<TSchema>(Func<MethodModelContext, TSchema> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddMethodSchema(
            schema: (c, _) => schema(c),
            when: when,
            where: where,
            order: order
        );

        public void AddMethodSchema<TSchema>(Func<MethodModelContext, ComponentContext, TSchema> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        )
        {
            when ??= c => true;
            where ??= c => true;
            order = order.ThemeDefault.Add;

            conventions.AddMethodAttribute(
                attribute: c => new GeneratorAttribute<TSchema>
                {
                    Generator = cc => cc.Trace.CaptureDescriptor(c, cc, () => schema(c, cc), orderInfo: $"+{order}"),
                    Filter = where,
                    Trace = c.Trace
                },
                when: c => c.Type.Has<ControllerModelAttribute>() && c.Method.Has<ActionModelAttribute>() && when(c),
                beforeBuildingIndexes: false,
                order: order
            );
        }

        public void RemoveMethodSchema<TSchema>(Func<MethodModelContext, bool> when,
            Order order = default
        )
        {
            conventions.RemoveMethodAttribute<GeneratorAttribute<TSchema>>(when: when,
                beforeBuildingIndexes: false,
                order: order.ThemeDefault.Add
            );
        }

        public void AddParameterSchema<TSchema>(Func<TSchema> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddParameterSchema(
            schema: _ => schema(),
            when: when,
            where: where,
            order: order
        );

        public void AddParameterSchema<TSchema>(Func<ParameterModelContext, TSchema> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddParameterSchema(
            schema: (c, _) => schema(c),
            when: when,
            where: where,
            order: order
        );

        public void AddParameterSchema<TSchema>(Func<ParameterModelContext, ComponentContext, TSchema> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        )
        {
            when ??= c => true;
            where ??= c => true;
            order = order.ThemeDefault.Add;

            conventions.AddParameterAttribute(
                attribute: c => new GeneratorAttribute<TSchema>
                {
                    Generator = cc => cc.Trace.CaptureDescriptor(c, cc, () => schema(c, cc), orderInfo: $"+{order}"),
                    Filter = where,
                    Trace = c.Trace
                },
                when: c => c.Type.Has<ControllerModelAttribute>() && c.Parameter.Has<ParameterModelAttribute>() && when(c),
                beforeBuildingIndexes: false,
                order: order
            );
        }

        public void RemoveParameterSchema<TSchema>(Func<ParameterModelContext, bool> when,
            Order order = default
        )
        {
            conventions.RemoveParameterAttribute<GeneratorAttribute<TSchema>>(when: when,
                beforeBuildingIndexes: false,
                order: order.ThemeDefault.Add
            );
        }

        public void AddTypeSchemaConfiguration<TSchema>(Action<TSchema> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddTypeSchemaConfiguration<TSchema>((s, _) => schema(s),
            when: when,
            where: where,
            order: order
        );

        public void AddTypeSchemaConfiguration<TSchema>(Action<TSchema, TypeModelMetadataContext> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddTypeSchemaConfiguration<TSchema>((s, c, _) => schema(s, c),
            when: when,
            where: where,
            order: order
        );

        public void AddTypeSchemaConfiguration<TSchema>(Action<TSchema, TypeModelMetadataContext, ComponentContext> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        )
        {
            when ??= _ => true;
            where ??= _ => true;
            order = order.ThemeDefault.Configure;

            conventions.AddTypeAttributeConfiguration<GeneratorAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapGenerator(
                    context: c,
                    apply: (s, cc) => schema(s, c, cc),
                    where: where,
                    order: order
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddPropertySchemaConfiguration<TSchema>(Action<TSchema> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddPropertySchemaConfiguration<TSchema>((s, _) => schema(s),
            when: when,
            where: where,
            order: order
        );

        public void AddPropertySchemaConfiguration<TSchema>(Action<TSchema, PropertyModelContext> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddPropertySchemaConfiguration<TSchema>((s, c, _) => schema(s, c),
            when: when,
            where: where,
            order: order
        );

        public void AddPropertySchemaConfiguration<TSchema>(Action<TSchema, PropertyModelContext, ComponentContext> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        )
        {
            when ??= _ => true;
            where ??= _ => true;
            order = order.ThemeDefault.Configure;

            conventions.AddPropertyAttributeConfiguration<GeneratorAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapGenerator(
                    context: c,
                    apply: (s, cc) => schema(s, c, cc),
                    where: where,
                    order: order
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddMethodSchemaConfiguration<TSchema>(Action<TSchema> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddMethodSchemaConfiguration<TSchema>((s, _) => schema(s),
            when: when,
            where: where,
            order: order
        );

        public void AddMethodSchemaConfiguration<TSchema>(Action<TSchema, MethodModelContext> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddMethodSchemaConfiguration<TSchema>((s, c, _) => schema(s, c),
            when: when,
            where: where,
            order: order
        );

        public void AddMethodSchemaConfiguration<TSchema>(Action<TSchema, MethodModelContext, ComponentContext> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        )
        {
            when ??= _ => true;
            where ??= _ => true;
            order = order.ThemeDefault.Configure;

            conventions.AddMethodAttributeConfiguration<GeneratorAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapGenerator(
                    context: c,
                    apply: (s, cc) => schema(s, c, cc),
                    where: where,
                    order: order
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddParameterSchemaConfiguration<TSchema>(Action<TSchema> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddParameterSchemaConfiguration<TSchema>((s, _) => schema(s),
            when: when,
            where: where,
            order: order
        );

        public void AddParameterSchemaConfiguration<TSchema>(Action<TSchema, ParameterModelContext> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) => conventions.AddParameterSchemaConfiguration<TSchema>((s, c, _) => schema(s, c),
            when: when,
            where: where,
            order: order
        );

        public void AddParameterSchemaConfiguration<TSchema>(Action<TSchema, ParameterModelContext, ComponentContext> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        )
        {
            when ??= _ => true;
            where ??= _ => true;
            order = order.ThemeDefault.Configure;

            conventions.AddParameterAttributeConfiguration<GeneratorAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapGenerator(
                    context: c,
                    apply: (s, cc) => schema(s, c, cc),
                    where: where,
                    order: order
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddTypeComponent<TSchema>(Func<ComponentDescriptor<TSchema>> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddTypeComponent(
                component: _ => component(),
                when: when,
                where: where,
                order: order
            );

        public void AddTypeComponent<TSchema>(Func<TypeModelMetadataContext, ComponentDescriptor<TSchema>> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddTypeComponent(
                component: (c, _) => component(c),
                when: when,
                where: where,
                order: order
            );

        public void AddTypeComponent<TSchema>(Func<TypeModelMetadataContext, ComponentContext, ComponentDescriptor<TSchema>> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            when ??= c => true;
            where ??= c => true;
            order = order.ThemeDefault.Add;

            conventions.AddTypeAttribute(
                apply: (c, add) =>
                {
                    add(c.Type, new ComponentGeneratorAttribute<TSchema>
                    {
                        Generator = cc => cc.Trace.CaptureDescriptor(c, cc, () => component(c, cc), orderInfo: $"+{order}"),
                        Filter = where,
                        Trace = c.Trace
                    });
                    add(c.Type, new ContextBasedComponentAttribute(typeof(TSchema))
                    {
                        Filter = where
                    });
                },
                when: c => when(c),
                beforeBuildingIndexes: false,
                order: order
            );
        }

        public void RemoveTypeComponent<TSchema>(Func<TypeModelMetadataContext, bool> when,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            conventions.RemoveTypeAttribute<ComponentGeneratorAttribute<TSchema>>(when,
                beforeBuildingIndexes: false,
                order: order.ThemeDefault.Add
            );
        }

        public void AddPropertyComponent<TSchema>(Func<ComponentDescriptor<TSchema>> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddPropertyComponent(
                component: _ => component(),
                when: when,
                where: where,
                order: order
            );

        public void AddPropertyComponent<TSchema>(Func<PropertyModelContext, ComponentDescriptor<TSchema>> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddPropertyComponent(
                component: (c, _) => component(c),
                when: when,
                where: where,
                order: order
            );

        public void AddPropertyComponent<TSchema>(Func<PropertyModelContext, ComponentContext, ComponentDescriptor<TSchema>> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            when ??= c => true;
            where ??= c => true;
            order = order.ThemeDefault.Add;

            conventions.AddPropertyAttribute(
                apply: (c, add) =>
                {
                    add(c.Property, new ComponentGeneratorAttribute<TSchema>
                    {
                        Generator = cc => cc.Trace.CaptureDescriptor(c, cc, () => component(c, cc), orderInfo: $"+{order}"),
                        Filter = where,
                        Trace = c.Trace
                    });
                    add(c.Property, new ContextBasedComponentAttribute(typeof(TSchema))
                    {
                        Filter = where
                    });
                },
                when: c => when(c),
                beforeBuildingIndexes: false,
                order: order
            );
        }

        public void RemovePropertyComponent<TSchema>(Func<PropertyModelContext, bool> when,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            conventions.RemovePropertyAttribute<ComponentGeneratorAttribute<TSchema>>(when,
                beforeBuildingIndexes: false,
                order: order.ThemeDefault.Add
            );
        }

        public void AddMethodComponent<TSchema>(Func<ComponentDescriptor<TSchema>> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddMethodComponent(
                component: _ => component(),
                when: when,
                where: where,
                order: order
            );

        public void AddMethodComponent<TSchema>(Func<MethodModelContext, ComponentDescriptor<TSchema>> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddMethodComponent(
                component: (c, _) => component(c),
                when: when,
                where: where,
                order: order
            );

        public void AddMethodComponent<TSchema>(Func<MethodModelContext, ComponentContext, ComponentDescriptor<TSchema>> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            when ??= c => true;
            where ??= cc => true;
            order = order.ThemeDefault.Add;

            conventions.AddMethodAttribute(
                apply: (c, add) =>
                {
                    add(c.Method, new ComponentGeneratorAttribute<TSchema>
                    {
                        Generator = cc => cc.Trace.CaptureDescriptor(c, cc, () => component(c, cc), orderInfo: $"+{order}"),
                        Filter = where,
                        Trace = c.Trace
                    });
                    add(c.Method, new ContextBasedComponentAttribute(typeof(TSchema))
                    {
                        Filter = where
                    });
                },
                when: c => c.Type.Has<ControllerModelAttribute>() && c.Method.Has<ActionModelAttribute>() && when(c),
                beforeBuildingIndexes: false,
                order: order
            );
        }

        public void RemoveMethodComponent<TSchema>(Func<MethodModelContext, bool> when,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            conventions.RemoveMethodAttribute<ComponentGeneratorAttribute<TSchema>>(when,
                beforeBuildingIndexes: false,
                order: order.ThemeDefault.Add
            );
        }

        public void AddParameterComponent<TSchema>(Func<ComponentDescriptor<TSchema>> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddParameterComponent(
                component: _ => component(),
                when: when,
                where: where,
                order: order
            );

        public void AddParameterComponent<TSchema>(Func<ParameterModelContext, ComponentDescriptor<TSchema>> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddParameterComponent(
                component: (c, _) => component(c),
                when: when,
                where: where,
                order: order
            );

        public void AddParameterComponent<TSchema>(Func<ParameterModelContext, ComponentContext, ComponentDescriptor<TSchema>> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            when ??= c => true;
            where ??= c => true;
            order = order.ThemeDefault.Add;

            conventions.AddParameterAttribute(
                apply: (c, add) =>
                {
                    add(c.Parameter, new ComponentGeneratorAttribute<TSchema>
                    {
                        Generator = cc => cc.Trace.CaptureDescriptor(c, cc, () => component(c, cc), orderInfo: $"+{order}"),
                        Filter = where,
                        Trace = c.Trace
                    });
                    add(c.Parameter, new ContextBasedComponentAttribute(typeof(TSchema))
                    {
                        Filter = where
                    });
                },
                when: c => c.Type.Has<ControllerModelAttribute>() && c.Parameter.Has<ParameterModelAttribute>() && when(c),
                beforeBuildingIndexes: false,
                order: order
            );
        }

        public void RemoveParameterComponent<TSchema>(Func<ParameterModelContext, bool> when,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            conventions.RemoveParameterAttribute<ComponentGeneratorAttribute<TSchema>>(when,
                beforeBuildingIndexes: false,
                order: order.ThemeDefault.Add
            );
        }

        public void AddTypeComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddTypeComponentConfiguration<TSchema>((s, _) => component(s),
                when: when,
                where: where,
                order: order
            );

        public void AddTypeComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, TypeModelMetadataContext> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddTypeComponentConfiguration<TSchema>((s, c, _) => component(s, c),
                when: when,
                where: where,
                order: order
            );

        public void AddTypeComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, TypeModelMetadataContext, ComponentContext> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            when ??= _ => true;
            where ??= _ => true;
            order = order.ThemeDefault.Configure;

            conventions.AddTypeAttributeConfiguration<ComponentGeneratorAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapGenerator(
                    context: c,
                    apply: (d, cc) => component(d, c, cc),
                    where: where,
                    order: order
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddPropertyComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddPropertyComponentConfiguration<TSchema>((s, _) => component(s),
                when: when,
                where: where,
                order: order
            );

        public void AddPropertyComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, PropertyModelContext> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddPropertyComponentConfiguration<TSchema>((s, c, _) => component(s, c),
                when: when,
                where: where,
                order: order
            );

        public void AddPropertyComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, PropertyModelContext, ComponentContext> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            when ??= _ => true;
            where ??= _ => true;
            order = order.ThemeDefault.Configure;

            conventions.AddPropertyAttributeConfiguration<ComponentGeneratorAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapGenerator(
                    context: c,
                    apply: (d, cc) => component(d, c, cc),
                    where: where,
                    order: order
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddMethodComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddMethodComponentConfiguration<TSchema>((s, _) => component(s),
                when: when,
                where: where,
                order: order
            );

        public void AddMethodComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, MethodModelContext> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddMethodComponentConfiguration<TSchema>((s, c, _) => component(s, c),
                when: when,
                where: where,
                order: order
            );

        public void AddMethodComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, MethodModelContext, ComponentContext> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            when ??= _ => true;
            where ??= _ => true;
            order = order.ThemeDefault.Configure;

            conventions.AddMethodAttributeConfiguration<ComponentGeneratorAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapGenerator(
                    context: c,
                    apply: (d, cc) => component(d, c, cc),
                    where: where,
                    order: order
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddParameterComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddParameterComponentConfiguration<TSchema>((s, _) => component(s),
                when: when,
                where: where,
                order: order
            );

        public void AddParameterComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, ParameterModelContext> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddParameterComponentConfiguration<TSchema>((s, c, _) => component(s, c),
                when: when,
                where: where,
                order: order
            );

        public void AddParameterComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, ParameterModelContext, ComponentContext> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Order order = default
        ) where TSchema : IComponentSchema
        {
            when ??= _ => true;
            where ??= _ => true;
            order = order.ThemeDefault.Configure;

            conventions.AddParameterAttributeConfiguration<ComponentGeneratorAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapGenerator(
                    context: c,
                    apply: (d, cc) => component(d, c, cc),
                    where: where,
                    order: order
                ),
                when: c => when(c),
                order: order
            );
        }
    }

    extension(TypeModel type)
    {
        public MethodModel GetMethod(string name) =>
            type.GetMembers().Methods[name];

        public IEnumerable<string> GetEnumNames() =>
            [.. type.Apply(t => Enum.GetNames(t).Select(n => n.TrimStart('_')))];
    }

    extension(MethodModel method)
    {
        public ActionModelAttribute GetAction() =>
            method.Get<ActionModelAttribute>();
    }

    extension(ActionModelAttribute action)
    {
        public string GetRoute(List<(string key, string value)> routeParameters)
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
    }

    extension(MethodOverloadModel methodOverload)
    {
        public bool ReturnsList() =>
            methodOverload.ReturnType.SkipTask().IsAssignableTo<IList>();

        public bool ReturnsVoid() =>
            methodOverload.ReturnType.Is(typeof(void)) || methodOverload.ReturnType.Is<Task>();
    }

    extension(Page.Generator _)
    {
        public PageBuilder Method<TDomainType, TPageSchema>(string methodName) where TPageSchema : IPageSchema =>
            context =>
            {
                var (domain, l) = context;

                if (!domain.Types[typeof(TDomainType)].TryGetMembers(out var members))
                {
                    throw DiagnosticCode.RequiresBuildLevel.Exception(
                        $"{typeof(TDomainType).Name}.{methodName} cannot be used as a page source, because members of {typeof(TDomainType).Name} are not included in domain model"
                    );
                }

                if (!members.Methods.TryGetValue(methodName, out var method))
                {
                    throw DiagnosticCode.MethodRequired.Exception(
                        $"{typeof(TDomainType).Name} does not have a method named '{methodName}'"
                    );
                }

                return method.GenerateRequiredComponent<TPageSchema>(context.Drill(nameof(Page), typeof(TDomainType).Name, method.Name));
            };

        public PageBuilder Type<TDomainType, TPageSchema>() where TPageSchema : IPageSchema =>
            context =>
            {
                var (domain, l) = context;

                if (!domain.Types[typeof(TDomainType)].TryGetMetadata(out var metadata))
                {
                    throw DiagnosticCode.RequiresBuildLevel.Exception(
                        $"{typeof(TDomainType).Name} cannot be used as a page source, because its metadata is not included in domain model"
                    );
                }

                return metadata.GenerateRequiredComponent<TPageSchema>(context.Drill(nameof(Page), typeof(TDomainType).Name));
            };
    }

    extension(IComponentSchema schema)
    {
        public TComponentSchema As<TComponentSchema>() where TComponentSchema : IComponentSchema =>
            (TComponentSchema)schema;
    }

    extension(IData data)
    {
        public TData As<TData>() where TData : IData =>
            (TData)data;
    }

    extension<T>(IEnumerable<T> enumerable)
    {
        public IEnumerable<T> WhereAppliesTo(ComponentContext context) =>
            enumerable.Where(c => c is not IComponentContextFilter when || when.AppliesTo(context));
    }

    extension(PageDescriptors pages)
    {
        public void AddPages(IEnumerable<Route> routes, DomainModel domain, NewLocaleKey l,
            Action<DiagnosticsResult>? onComplete = default,
            ComponentPath.Debug? debugComponentPaths = default
        )
        {
            using (Diagnostics.Start(nameof(PageDescriptors), onDispose: onComplete))
            {
                var sitemap = routes.ToImmutableList();
                foreach (var route in routes)
                {
                    var page = route.BuildPage(new()
                    {
                        Route = route,
                        Sitemap = sitemap,
                        Domain = domain,
                        NewLocaleKey = l
                    });
                    if (page is null) { continue; }

                    pages.Add(page);
                }

                if (debugComponentPaths is not null)
                {
                    Diagnostics.Current.ReportInfo(ComponentPath.GetPathsAsTree(debugComponentPaths));
                }
            }
        }
    }

    extension<TSchema>(GeneratorAttribute<TSchema> attribute)
    {
        // WARNING
        //
        // Do NOT remove this warning disable section unintentionally.
        // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0051
        // NOTE
        //
        // This is refactored to remove duplication in below conventions but
        // compromises code readability. It basically wraps the given generator
        // and applies given function only when given filter (`when`) passes.
        //
        // Filter is applied within the function because it is the only
        // way to access to the component context.
        void WrapGenerator(
            DomainModelContext context,
            Func<ComponentContext, bool> where,
            Action<TSchema, ComponentContext> apply,
            Order order
        )
        {
            var prev = attribute.Generator;
            var trace = context.Trace;

            attribute.Generator = cc =>
            {
                var result = prev(cc);
                if (!where(cc)) { return result; }

                return trace.CaptureDescriptor(context, cc, result, () => apply(result, cc), orderInfo: $"+{order}");
            };
        }
#pragma warning restore IDE0051
    }

    // WARNING
    //
    // Do NOT remove this warning disable section unintentionally.
    // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0051
    static bool WarnForMissingComponent => Environment.GetCommandLineArgs().Contains("--warn-for-missing-component");
#pragma warning restore IDE0051

    extension(ICustomAttributesModel metadata)
    {
        public List<TSchema> GenerateSchemas<TSchema>(ComponentContext context)
        {
            if (!metadata.TryGetAll<GeneratorAttribute<TSchema>>(out var generators)) { return []; }

            return
            [
                .. generators
                    .WhereAppliesTo(context)
                    .Cast<IComponentContextBasedGenerator<TSchema>>()
                    .Select(b => b.Generate(context))
            ];
        }

        public TSchema GenerateRequiredSchema<TSchema>(ComponentContext context) =>
            metadata.GenerateSchema<TSchema>(context) ??
            throw DiagnosticCode.MissingRequiredSchema.Exception(
                $"`{metadata.CustomAttributes.Name}` doesn't have descriptor for schema type `{typeof(TSchema).Name}` at path `{context.Path}`"
            );

        public TSchema? GenerateSchema<TSchema>(ComponentContext context)
        {
            if (!metadata.TryGetAll<GeneratorAttribute<TSchema>>(out var generators)) { return default; }

            var generator = generators
                .WhereAppliesTo(context)
                .Cast<IComponentContextBasedGenerator<TSchema>>()
                .LastOrDefault();
            if (generator is null) { return default; }

            return generator.Generate(context);
        }

        public ComponentDescriptor<T> GenerateRequiredComponent<T>(ComponentContext context) where T : IComponentSchema =>
            metadata.GenerateRequiredComponent(context, componentType: typeof(T), omitWarningMessage: true) as ComponentDescriptor<T> ??
            throw DiagnosticCode.MissingRequiredComponentOfType.Exception(
                $"`{metadata.CustomAttributes.Name}` doesn't have a component descriptor of type `{typeof(T).Name}` at path `{context.Path}`"
            );

        public IComponentDescriptor GenerateRequiredComponent(ComponentContext context,
            Type? componentType = default,
            bool omitWarningMessage = false
        )
        {
            var result = metadata.GenerateComponent(context, componentType: componentType);
            if (result is not null) { return result; }

            if (!omitWarningMessage)
            {
                var message =
                    $"`{metadata.CustomAttributes.Name}` doesn't have any component descriptor" +
                    $"{(componentType is null ? string.Empty : $" of type {componentType.Name}")}" +
                    $" at path `{context.Path}`";

                if (WarnForMissingComponent) { Diagnostics.Current.ReportWarning(DiagnosticCode.MissingRequiredComponent, message); }
                else { Diagnostics.Current.ReportError(DiagnosticCode.MissingRequiredComponent, message); }
            }

            return DomainComponents.CustomAttributesMissingComponent(metadata, context, options: mc => mc.Component = componentType?.Name);
        }

        public ComponentDescriptor<T>? GenerateComponent<T>(ComponentContext context) where T : IComponentSchema =>
            metadata.GenerateComponent(context, componentType: typeof(T)) as ComponentDescriptor<T>;

        public IComponentDescriptor? GenerateComponent(ComponentContext context,
            Type? componentType = default
        )
        {
            if (!metadata.TryGetAll<ContextBasedComponentAttribute>(out var contextBasedComponents)) { return default; }

            foreach (var contextBasedComponent in contextBasedComponents.WhereAppliesTo(context).Where(cbc => componentType is null || cbc.SchemaType == componentType).Reverse())
            {
                var generatorType = typeof(ComponentGeneratorAttribute<>).MakeGenericType(contextBasedComponent.SchemaType);
                if (!metadata.TryGetAll(generatorType, out var generators)) { continue; }

                var generator = generators
                    .Cast<IComponentContextBasedGenerator<IComponentDescriptor>>()
                    .WhereAppliesTo(context)
                    .LastOrDefault();
                if (generator is null) { continue; }

                return generator.Generate(context);
            }

            return default;
        }
    }

    extension(Inspect inspect)
    {
        public void TypeComponent<T>(
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? component = default
        ) where T : IComponentSchema =>
            inspect.Component(
                when: when.GeneralizeOrDefault(),
                where: where,
                component: component
            );

        public void PropertyComponent<T>(
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? component = default
        ) where T : IComponentSchema =>
            inspect.Component(
                when: when.GeneralizeOrDefault(),
                where: where,
                component: component
            );

        public void MethodComponent<T>(
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? component = default
        ) where T : IComponentSchema =>
            inspect.Component(
                when: when.GeneralizeOrDefault(),
                where: where,
                component: component
            );

        public void ParameterComponent<T>(
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? component = default
        ) where T : IComponentSchema =>
            inspect.Component(
                when: when.GeneralizeOrDefault(),
                where: where,
                component: component
            );

        public void Component<T>(
            Func<DomainModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? component = default
        ) where T : IComponentSchema
        {
            component ??= x => x;

            inspect.Schema(component.Compile(), component.ToString(),
                when: when,
                where: where
            );
        }

        public void TypeSchema<T>(
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? schema = default
        ) => inspect.Schema(
                when: when.GeneralizeOrDefault(),
                where: where,
                schema: schema
            );

        public void PropertySchema<T>(
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? schema = default
        ) => inspect.Schema(
                when: when.GeneralizeOrDefault(),
                where: where,
                schema: schema
            );

        public void MethodSchema<T>(
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? schema = default
        ) => inspect.Schema(
                when: when.GeneralizeOrDefault(),
                where: where,
                schema: schema
            );

        public void ParameterSchema<T>(
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? schema = default
        ) => inspect.Schema(
                when: when.GeneralizeOrDefault(),
                where: where,
                schema: schema
            );

        public void Schema<T>(
            Func<DomainModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            Expression<Func<T, object?>>? schema = default
        )
        {
            schema ??= x => x;

            inspect.Schema(schema.Compile(), schema.ToString(),
                when: when,
                where: where
            );
        }

        // WARNING
        //
        // Do NOT remove this warning disable section unintentionally.
        // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0051
        void Schema<T>(Func<T, object?> evaluate, string expression,
            Func<DomainModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default
        )
        {
            Inspection.Current = new(typeof(T), c => evaluate((T)c), expression);
            if (when is not null) { Inspection.Current.AddFilter(nameof(when), when); }
            if (where is not null) { Inspection.Current.AddFilter(nameof(where), where); }
        }
#pragma warning restore IDE0051
    }

    extension(Trace trace)
    {
        // WARNING
        //
        // Do NOT remove this warning disable section unintentionally.
        // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0051
        static bool ShouldCapture(DomainModelContext c, ComponentContext cc, [NotNullWhen(true)] out Inspection? inspection)
        {
            inspection = Inspection.Current;

            return
                inspection is not null &&
                (
                    !inspection.TryGetFilter<Func<DomainModelContext, bool>>("when", out var when) ||
                    when(c)
                ) &&
                (
                    !inspection.TryGetFilter<Func<ComponentContext, bool>>("where", out var where) ||
                    where(cc)
                );
        }
#pragma warning restore IDE0051

        public T CaptureDescriptor<T>(DomainModelContext c, ComponentContext cc, Func<T> create,
            string? orderInfo = default
        )
        {
            if (!ShouldCapture(c, cc, out var inspection))
            {
                return create();
            }

            return new Capture<T>(inspection, trace.StackTrace, create, new DescriptorCaptureType(cc, orderInfo)).Execute();
        }

        public T CaptureDescriptor<T>(DomainModelContext c, ComponentContext cc, T target, Action update,
            string? orderInfo = default
        )
        {
            if (!ShouldCapture(c, cc, out var inspection))
            {
                update();

                return target;
            }

            return new Capture<T>(inspection, trace.StackTrace, update, new DescriptorCaptureType(cc, orderInfo), target).Execute();
        }
    }

    extension(Stubber giveMe)
    {
        public PageContext APageContext(
            string? path = default,
            string? title = default
        )
        {
            path ??= "/";
            title ??= "TEST PAGE";

            return new()
            {
                Route = new(path, title),
                Sitemap = [],
                Domain = giveMe.TheDomainModel(),
                NewLocaleKey = s => s
            };
        }

        public ComponentContext AComponentContext(
            object[]? paths = default
        )
        {
            paths ??= [];

            return giveMe
                .APageContext()
                .Drill(paths);
        }
    }
}