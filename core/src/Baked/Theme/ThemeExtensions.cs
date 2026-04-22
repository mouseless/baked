using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi;
using Baked.RestApi.Model;
using Baked.Testing;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Baked.Ui.Configuration;
using System.Collections.Immutable;

using static Baked.Ui.Datas;

namespace Baked;

public static class ThemeExtensions
{
    extension(List<IFeature> features)
    {
        public void AddTheme(FeatureFunc<ThemeConfigurator> configure) =>
            features.Add(configure(new()));
    }

    extension(DiagnosticsCode)
    {
        public static DiagnosticsCode MissingRequiredComponent => new(101, "missing-required-component");
        public static DiagnosticsCode MissingRequiredComponentOfType => new(102, "missing-required-component-of-type");
        public static DiagnosticsCode MissingRequiredSchema => new(103, "missing-required-schema");
        public static DiagnosticsCode RequiresLocateAction => new(104, "requires-locate-action");
        public static DiagnosticsCode MethodRequired => new(105, "method-required");
        public static DiagnosticsCode MissingItem => new(106, "missing-item");
        public static DiagnosticsCode InvalidState => new(107, "invalid-state");
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

        public Route Root(string path, string title, string icon) =>
            router.Create(path, title) with { Icon = icon, SideMenu = true, ErrorSafeLink = true };

        public Route RootDynamic(string path, string title) =>
            router.Create(path, title) with { ErrorSafeLink = false, SideMenu = false };

        public Route Child(string path, string title, string parentPath) =>
            router.Create(path, title) with { ParentPath = parentPath };

        public Route ChildDynamic(string path, string title, string parentPath) =>
            router.Create(path, title) with { ParentPath = parentPath, ErrorSafeLink = false, SideMenu = false };
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
                        throw DiagnosticsCode.RequiresLocateAction.Exception(
                            $"`{c.Type.Name}` should have `Locate` action added"
                        );
                    }

                    return Remote(locate.GetRoute(), o => o.Params = Computed.UseRoute("params"));
                }
            );
        }

        public void AddTypeSchema<TSchema>(Func<TSchema> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddTypeSchema(
            schema: _ => schema(),
            when: when,
            where: where,
            order: order
        );

        public void AddTypeSchema<TSchema>(Func<TypeModelMetadataContext, TSchema> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddTypeSchema(
            schema: (c, _) => schema(c),
            when: when,
            where: where,
            order: order
        );

        public void AddTypeSchema<TSchema>(Func<TypeModelMetadataContext, ComponentContext, TSchema> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        )
        {
            when ??= c => true;
            where ??= c => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.AddTypeAttribute(
                attribute: c => new DescriptorBuilderAttribute<TSchema>()
                {
                    Builder = cc => schema(c, cc),
                    Filter = where
                },
                when: when,
                requiresIndex: false,
                order: order
            );
        }

        public void RemoveTypeSchema<TSchema>(Func<TypeModelMetadataContext, bool> when,
            int order = default
        )
        {
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.RemoveTypeAttribute<DescriptorBuilderAttribute<TSchema>>(when: when,
                requiresIndex: false,
                order: order
            );
        }

        public void AddPropertySchema<TSchema>(Func<TSchema> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddPropertySchema(
            schema: _ => schema(),
            when: when,
            where: where,
            order: order
        );

        public void AddPropertySchema<TSchema>(Func<PropertyModelContext, TSchema> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddPropertySchema(
            schema: (c, _) => schema(c),
            when: when,
            where: where,
            order: order
        );

        public void AddPropertySchema<TSchema>(Func<PropertyModelContext, ComponentContext, TSchema> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        )
        {
            when ??= c => true;
            where ??= c => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.AddPropertyAttribute(
                attribute: c => new DescriptorBuilderAttribute<TSchema>()
                {
                    Builder = cc => schema(c, cc),
                    Filter = where
                },
                when: when,
                requiresIndex: false,
                order: order
            );
        }

        public void RemovePropertySchema<TSchema>(Func<PropertyModelContext, bool> when,
            int order = default
        )
        {
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.RemovePropertyAttribute<DescriptorBuilderAttribute<TSchema>>(when: when,
                requiresIndex: false,
                order: order
            );
        }

        public void AddMethodSchema<TSchema>(Func<TSchema> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddMethodSchema(
            schema: _ => schema(),
            when: when,
            where: where,
            order: order
        );

        public void AddMethodSchema<TSchema>(Func<MethodModelContext, TSchema> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddMethodSchema(
            schema: (c, _) => schema(c),
            when: when,
            where: where,
            order: order
        );

        public void AddMethodSchema<TSchema>(Func<MethodModelContext, ComponentContext, TSchema> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        )
        {
            when ??= c => true;
            where ??= c => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.AddMethodAttribute(
                attribute: c => new DescriptorBuilderAttribute<TSchema>()
                {
                    Builder = cc => schema(c, cc),
                    Filter = where
                },
                when: c => c.Type.Has<ControllerModelAttribute>() && c.Method.Has<ActionModelAttribute>() && when(c),
                requiresIndex: false,
                order: order
            );
        }

        public void RemoveMethodSchema<TSchema>(Func<MethodModelContext, bool> when,
            int order = default
        )
        {
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.RemoveMethodAttribute<DescriptorBuilderAttribute<TSchema>>(when: when,
                requiresIndex: false,
                order: order
            );
        }

        public void AddParameterSchema<TSchema>(Func<TSchema> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddParameterSchema(
            schema: _ => schema(),
            when: when,
            where: where,
            order: order
        );

        public void AddParameterSchema<TSchema>(Func<ParameterModelContext, TSchema> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddParameterSchema(
            schema: (c, _) => schema(c),
            when: when,
            where: where,
            order: order
        );

        public void AddParameterSchema<TSchema>(Func<ParameterModelContext, ComponentContext, TSchema> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        )
        {
            when ??= c => true;
            where ??= c => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.AddParameterAttribute(
                attribute: c => new DescriptorBuilderAttribute<TSchema>()
                {
                    Builder = cc => schema(c, cc),
                    Filter = where
                },
                when: c => c.Type.Has<ControllerModelAttribute>() && c.Parameter.Has<ParameterModelAttribute>() && when(c),
                requiresIndex: false,
                order: order
            );
        }

        public void RemoveParameterSchema<TSchema>(Func<ParameterModelContext, bool> when,
            int order = default
        )
        {
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.RemoveParameterAttribute<DescriptorBuilderAttribute<TSchema>>(when: when,
                requiresIndex: false,
                order: order
            );
        }

        public void AddTypeSchemaConfiguration<TSchema>(Action<TSchema> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddTypeSchemaConfiguration<TSchema>((s, _) => schema(s),
            when: when,
            where: where,
            order: order
        );

        public void AddTypeSchemaConfiguration<TSchema>(Action<TSchema, TypeModelMetadataContext> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddTypeSchemaConfiguration<TSchema>((s, c, _) => schema(s, c),
            when: when,
            where: where,
            order: order
        );

        public void AddTypeSchemaConfiguration<TSchema>(Action<TSchema, TypeModelMetadataContext, ComponentContext> schema,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        )
        {
            when ??= _ => true;
            where ??= _ => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit * 2;

            conventions.AddTypeAttributeConfiguration<DescriptorBuilderAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapBuilder(
                    apply: (s, cc) => schema(s, c, cc),
                    when: where
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddPropertySchemaConfiguration<TSchema>(Action<TSchema> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddPropertySchemaConfiguration<TSchema>((s, _) => schema(s),
            when: when,
            where: where,
            order: order
        );

        public void AddPropertySchemaConfiguration<TSchema>(Action<TSchema, PropertyModelContext> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddPropertySchemaConfiguration<TSchema>((s, c, _) => schema(s, c),
            when: when,
            where: where,
            order: order
        );

        public void AddPropertySchemaConfiguration<TSchema>(Action<TSchema, PropertyModelContext, ComponentContext> schema,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        )
        {
            when ??= _ => true;
            where ??= _ => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit * 2;

            conventions.AddPropertyAttributeConfiguration<DescriptorBuilderAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapBuilder(
                    apply: (s, cc) => schema(s, c, cc),
                    when: where
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddMethodSchemaConfiguration<TSchema>(Action<TSchema> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddMethodSchemaConfiguration<TSchema>((s, _) => schema(s),
            when: when,
            where: where,
            order: order
        );

        public void AddMethodSchemaConfiguration<TSchema>(Action<TSchema, MethodModelContext> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddMethodSchemaConfiguration<TSchema>((s, c, _) => schema(s, c),
            when: when,
            where: where,
            order: order
        );

        public void AddMethodSchemaConfiguration<TSchema>(Action<TSchema, MethodModelContext, ComponentContext> schema,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        )
        {
            when ??= _ => true;
            where ??= _ => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit * 2;

            conventions.AddMethodAttributeConfiguration<DescriptorBuilderAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapBuilder(
                    apply: (s, cc) => schema(s, c, cc),
                    when: where
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddParameterSchemaConfiguration<TSchema>(Action<TSchema> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddParameterSchemaConfiguration<TSchema>((s, _) => schema(s),
            when: when,
            where: where,
            order: order
        );

        public void AddParameterSchemaConfiguration<TSchema>(Action<TSchema, ParameterModelContext> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) => conventions.AddParameterSchemaConfiguration<TSchema>((s, c, _) => schema(s, c),
            when: when,
            where: where,
            order: order
        );

        public void AddParameterSchemaConfiguration<TSchema>(Action<TSchema, ParameterModelContext, ComponentContext> schema,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        )
        {
            when ??= _ => true;
            where ??= _ => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit * 2;

            conventions.AddParameterAttributeConfiguration<DescriptorBuilderAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapBuilder(
                    apply: (s, cc) => schema(s, c, cc),
                    when: where
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddTypeComponent<TSchema>(Func<ComponentDescriptor<TSchema>> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
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
            int order = default
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
            int order = default
        ) where TSchema : IComponentSchema
        {
            when ??= c => true;
            where ??= c => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.AddTypeAttribute(
                apply: (c, add) =>
                {
                    add(c.Type, new ComponentDescriptorBuilderAttribute<TSchema>()
                    {
                        Builder = cc => component(c, cc),
                        Filter = where
                    });
                    add(c.Type, new ContextBasedComponentAttribute(typeof(TSchema))
                    {
                        Filter = where
                    });
                },
                when: c => when(c),
                requiresIndex: false,
                order: order
            );
        }

        public void RemoveTypeComponent<TSchema>(Func<TypeModelMetadataContext, bool> when,
            int order = default
        ) where TSchema : IComponentSchema
        {
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.RemoveTypeAttribute<ComponentDescriptorBuilderAttribute<TSchema>>(when,
                requiresIndex: false,
                order: order
            );
        }

        public void AddPropertyComponent<TSchema>(Func<ComponentDescriptor<TSchema>> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
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
            int order = default
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
            int order = default
        ) where TSchema : IComponentSchema
        {
            when ??= c => true;
            where ??= c => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.AddPropertyAttribute(
                apply: (c, add) =>
                {
                    add(c.Property, new ComponentDescriptorBuilderAttribute<TSchema>()
                    {
                        Builder = cc => component(c, cc),
                        Filter = where
                    });
                    add(c.Property, new ContextBasedComponentAttribute(typeof(TSchema))
                    {
                        Filter = where
                    });
                },
                when: c => when(c),
                requiresIndex: false,
                order: order
            );
        }

        public void RemovePropertyComponent<TSchema>(Func<PropertyModelContext, bool> when,
            int order = default
        ) where TSchema : IComponentSchema
        {
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.RemovePropertyAttribute<ComponentDescriptorBuilderAttribute<TSchema>>(when,
                requiresIndex: false,
                order: order
            );
        }

        public void AddMethodComponent<TSchema>(Func<ComponentDescriptor<TSchema>> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
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
            int order = default
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
            int order = default
        ) where TSchema : IComponentSchema
        {
            when ??= c => true;
            where ??= cc => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.AddMethodAttribute(
                apply: (c, add) =>
                {
                    add(c.Method, new ComponentDescriptorBuilderAttribute<TSchema>()
                    {
                        Builder = cc => component(c, cc),
                        Filter = where
                    });
                    add(c.Method, new ContextBasedComponentAttribute(typeof(TSchema))
                    {
                        Filter = where
                    });
                },
                when: c => c.Type.Has<ControllerModelAttribute>() && c.Method.Has<ActionModelAttribute>() && when(c),
                requiresIndex: false,
                order: order
            );
        }

        public void RemoveMethodComponent<TSchema>(Func<MethodModelContext, bool> when,
            int order = default
        ) where TSchema : IComponentSchema
        {
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.RemoveMethodAttribute<ComponentDescriptorBuilderAttribute<TSchema>>(when,
                requiresIndex: false,
                order: order
            );
        }

        public void AddParameterComponent<TSchema>(Func<ComponentDescriptor<TSchema>> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
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
            int order = default
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
            int order = default
        ) where TSchema : IComponentSchema
        {
            when ??= c => true;
            where ??= c => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.AddParameterAttribute(
                apply: (c, add) =>
                {
                    add(c.Parameter, new ComponentDescriptorBuilderAttribute<TSchema>()
                    {
                        Builder = cc => component(c, cc),
                        Filter = where
                    });
                    add(c.Parameter, new ContextBasedComponentAttribute(typeof(TSchema))
                    {
                        Filter = where
                    });
                },
                when: c => c.Type.Has<ControllerModelAttribute>() && c.Parameter.Has<ParameterModelAttribute>() && when(c),
                requiresIndex: false,
                order: order
            );
        }

        public void RemoveParameterComponent<TSchema>(Func<ParameterModelContext, bool> when,
            int order = default
        ) where TSchema : IComponentSchema
        {
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit;

            conventions.RemoveParameterAttribute<ComponentDescriptorBuilderAttribute<TSchema>>(when,
                requiresIndex: false,
                order: order
            );
        }

        public void AddTypeComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddTypeComponentConfiguration<TSchema>((s, _) => component(s),
                when: when,
                where: where,
                order: order
            );

        public void AddTypeComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, TypeModelMetadataContext> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddTypeComponentConfiguration<TSchema>((s, c, _) => component(s, c),
                when: when,
                where: where,
                order: order
            );

        public void AddTypeComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, TypeModelMetadataContext, ComponentContext> component,
            Func<TypeModelMetadataContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema
        {
            when ??= _ => true;
            where ??= _ => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit * 2;

            conventions.AddTypeAttributeConfiguration<ComponentDescriptorBuilderAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapBuilder(
                    apply: (d, cc) => component(d, c, cc),
                    when: where
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddPropertyComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddPropertyComponentConfiguration<TSchema>((s, _) => component(s),
                when: when,
                where: where,
                order: order
            );

        public void AddPropertyComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, PropertyModelContext> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddPropertyComponentConfiguration<TSchema>((s, c, _) => component(s, c),
                when: when,
                where: where,
                order: order
            );

        public void AddPropertyComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, PropertyModelContext, ComponentContext> component,
            Func<PropertyModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema
        {
            when ??= _ => true;
            where ??= _ => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit * 2;

            conventions.AddPropertyAttributeConfiguration<ComponentDescriptorBuilderAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapBuilder(
                    apply: (d, cc) => component(d, c, cc),
                    when: where
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddMethodComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddMethodComponentConfiguration<TSchema>((s, _) => component(s),
                when: when,
                where: where,
                order: order
            );

        public void AddMethodComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, MethodModelContext> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddMethodComponentConfiguration<TSchema>((s, c, _) => component(s, c),
                when: when,
                where: where,
                order: order
            );

        public void AddMethodComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, MethodModelContext, ComponentContext> component,
            Func<MethodModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema
        {
            when ??= _ => true;
            where ??= _ => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit * 2;

            conventions.AddMethodAttributeConfiguration<ComponentDescriptorBuilderAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapBuilder(
                    apply: (d, cc) => component(d, c, cc),
                    when: where
                ),
                when: c => when(c),
                order: order
            );
        }

        public void AddParameterComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddParameterComponentConfiguration<TSchema>((s, _) => component(s),
                when: when,
                where: where,
                order: order
            );

        public void AddParameterComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, ParameterModelContext> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema =>
            conventions.AddParameterComponentConfiguration<TSchema>((s, c, _) => component(s, c),
                when: when,
                where: where,
                order: order
            );

        public void AddParameterComponentConfiguration<TSchema>(Action<ComponentDescriptor<TSchema>, ParameterModelContext, ComponentContext> component,
            Func<ParameterModelContext, bool>? when = default,
            Func<ComponentContext, bool>? where = default,
            int order = default
        ) where TSchema : IComponentSchema
        {
            when ??= _ => true;
            where ??= _ => true;
            order += RestApiLayer.MaxConventionOrder + LayerBase.ConventionOrderLimit * 2;

            conventions.AddParameterAttributeConfiguration<ComponentDescriptorBuilderAttribute<TSchema>>(
                attribute: (attribute, c) => attribute.WrapBuilder(
                    apply: (d, cc) => component(d, c, cc),
                    when: where
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
                    throw DiagnosticsCode.RequiresBuildLevel.Exception(
                        $"{typeof(TDomainType).Name}.{methodName} cannot be used as a page source, because members of {typeof(TDomainType).Name} are not included in domain model"
                    );
                }

                if (!members.Methods.TryGetValue(methodName, out var method))
                {
                    throw DiagnosticsCode.MethodRequired.Exception(
                        $"{typeof(TDomainType).Name} does not have a method named '{methodName}'"
                    );
                }

                return method.GetRequiredComponent<TPageSchema>(context.Drill(nameof(Page), typeof(TDomainType).Name, method.Name));
            };

        public PageBuilder Type<TDomainType, TPageSchema>() where TPageSchema : IPageSchema =>
            context =>
            {
                var (domain, l) = context;

                if (!domain.Types[typeof(TDomainType)].TryGetMetadata(out var metadata))
                {
                    throw DiagnosticsCode.RequiresBuildLevel.Exception(
                        $"{typeof(TDomainType).Name} cannot be used as a page source, because its metadata is not included in domain model"
                    );
                }

                return metadata.GetRequiredComponent<TPageSchema>(context.Drill(nameof(Page), typeof(TDomainType).Name));
            };
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
            bool? debugComponentPaths = default
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

                if (debugComponentPaths == true)
                {
                    Diagnostics.ReportInfo(ComponentPath.GetPathsAsTree());
                }
            }
        }
    }

    extension<TSchema>(DescriptorBuilderAttribute<TSchema> attribute)
    {
        // WARNING
        //
        // Do NOT remove this warning disable section unintentionally.
        // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0051
        // NOTE
        //
        // This is refactored to remove duplication in below conventions but
        // compromises code readability. It basically wraps the given builder
        // and applies given function only when given filter (`when`) passes.
        //
        // Filter is applied within the function because it is the only
        // way to access to the component context.
        void WrapBuilder(
            Func<ComponentContext, bool> when,
            Action<TSchema, ComponentContext> apply
        )
        {
            var prev = attribute.Builder;

            attribute.Builder = cc =>
            {
                var result = prev(cc);

                if (when(cc))
                {
                    apply(result, cc);
                }

                return result;
            };
        }
#pragma warning restore IDE0051
    }

    extension(ICustomAttributesModel metadata)
    {
        public List<TSchema> GetSchemas<TSchema>(ComponentContext context)
        {
            if (!metadata.TryGetAll<DescriptorBuilderAttribute<TSchema>>(out var builders)) { return []; }

            return
            [
                .. builders
                    .WhereAppliesTo(context)
                    .Cast<IComponentContextBasedBuilder<TSchema>>()
                    .Select(b => b.Build(context))
            ];
        }

        public TSchema GetRequiredSchema<TSchema>(ComponentContext context) =>
            metadata.GetSchema<TSchema>(context) ??
            throw DiagnosticsCode.MissingRequiredSchema.Exception(
                $"`{metadata.CustomAttributes.Name}` doesn't have descriptor for schema type `{typeof(TSchema).Name}` at path `{context.Path}`"
            );

        public TSchema? GetSchema<TSchema>(ComponentContext context)
        {
            if (!metadata.TryGetAll<DescriptorBuilderAttribute<TSchema>>(out var builders)) { return default; }

            var builder = builders
                .WhereAppliesTo(context)
                .Cast<IComponentContextBasedBuilder<TSchema>>()
                .LastOrDefault();
            if (builder is null) { return default; }

            return builder.Build(context);
        }

        public ComponentDescriptor<T> GetRequiredComponent<T>(ComponentContext context) where T : IComponentSchema =>
            metadata.GetRequiredComponent(context, componentType: typeof(T), omitWarningMessage: true) as ComponentDescriptor<T> ??
            throw DiagnosticsCode.MissingRequiredComponentOfType.Exception(
                $"`{metadata.CustomAttributes.Name}` doesn't have a component descriptor of type `{typeof(T).Name}` at path `{context.Path}`"
            );

        public IComponentDescriptor GetRequiredComponent(ComponentContext context,
            Type? componentType = default,
            bool omitWarningMessage = false
        )
        {
            var result = metadata.GetComponent(context, componentType: componentType);
            if (result is not null) { return result; }

            if (!omitWarningMessage)
            {
                var message =
                    $"`{metadata.CustomAttributes.Name}` doesn't have any component descriptor" +
                    $"{(componentType is null ? string.Empty : $" of type {componentType.Name}")}" +
                    $" at path `{context.Path}`";

                if (WarnForMissingComponent) { Diagnostics.ReportWarning(DiagnosticsCode.MissingRequiredComponent, message); }
                else { Diagnostics.ReportError(DiagnosticsCode.MissingRequiredComponent, message); }
            }

            return DomainComponents.CustomAttributesMissingComponent(metadata, context, options: mc => mc.Component = componentType?.Name);
        }

        public ComponentDescriptor<T>? GetComponent<T>(ComponentContext context) where T : IComponentSchema =>
            metadata.GetComponent(context, componentType: typeof(T)) as ComponentDescriptor<T>;

        public IComponentDescriptor? GetComponent(ComponentContext context,
            Type? componentType = default
        )
        {
            if (!metadata.TryGetAll<ContextBasedComponentAttribute>(out var contextBasedComponents)) { return default; }

            foreach (var contextBasedComponent in contextBasedComponents.WhereAppliesTo(context).Where(cbc => componentType is null || cbc.SchemaType == componentType).Reverse())
            {
                var builderType = typeof(ComponentDescriptorBuilderAttribute<>).MakeGenericType(contextBasedComponent.SchemaType);
                if (!metadata.TryGetAll(builderType, out var builders)) { continue; }

                var builder = builders
                    .Cast<IComponentContextBasedBuilder<IComponentDescriptor>>()
                    .WhereAppliesTo(context)
                    .LastOrDefault();
                if (builder is null) { continue; }

                return builder.Build(context);
            }

            return default;
        }
    }

    // WARNING
    //
    // Do NOT remove this warning disable section unintentionally.
    // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0051
    static bool WarnForMissingComponent => Environment.GetCommandLineArgs().Contains("--warn-for-missing-component");
#pragma warning restore IDE0051
}