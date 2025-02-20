using Baked.Architecture;
using Baked.Business;
using Baked.UI;
using Humanizer;

namespace Baked.Theme.Admin;

public class AdminThemeFeature : IFeature<ThemeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(
                apply: (context, add) =>
                {
                    add(context.Type.GetMetadata(), new DetailAttribute(
                            name: context.Type.Name.Kebaberize().ToLowerInvariant(),
                            path: $$"""/{{context.Type.Name.Pluralize().Kebaberize().ToLowerInvariant()}}/{0}"""
                        )
                    );

                    foreach (var property in context.Type.GetMembers().Properties)
                    {
                        if (property.IsPublic)
                        {
                            property.CustomAttributes.Add(new DetailPropertyAttribute(key: property.Name.Camelize(), title: property.Name));
                            property.CustomAttributes.Add(new TableColumnAttribute());
                        }
                    }
                },
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<LocatableAttribute>() &&
                    members.Properties.Any(p => p.IsPublic),
                order: int.MaxValue
            );

            builder.Conventions.AddMethodMetadata(
            apply: (c, add) =>
            {
                var title = c.Method.Name.Replace("Get", string.Empty);

                add(c.Method, new TableAttribute(
                    title: title,
                    path: $$"""{{c.Type.GetSingle<DetailAttribute>().Path}}/{{title.ToLowerInvariant()}}"""
                )
                {
                    Columns = c.Method.DefaultOverload.ReturnType.GetGenerics().GenericTypeArguments.First()
                        .Model.GetMembers().Properties
                            .Where(p => p.Has<TableColumnAttribute>())
                            .Select(p => p.Name)
                            .ToList() ?? []
                });
            },
            when: c =>
                c.Type.Has<DetailAttribute>() &&
                c.Method.Name.StartsWith("Get") &&
                c.Method.DefaultOverload.IsPublic &&
                !c.Method.DefaultOverload.Parameters.Any() &&
                c.Method.DefaultOverload.ReturnType.IsAssignableTo<IList>() &&
                c.Method.DefaultOverload.ReturnType.TryGetGenerics(out var generics) &&
                generics.GenericTypeArguments.First().Model.TryGetMembers(out var members) &&
                members.Properties.Any(p => p.Has<TableColumnAttribute>()),
            order: int.MaxValue
         );
        });

        configurator.ConfigureComponentDescriptors(components =>
        {
            configurator.UsingDomainModel(domain =>
            {
                foreach (var type in domain.Types.Where(t => t.HasMetadata() && t.GetMetadata().Has<DetailAttribute>()))
                {
                    var componentDescriptor = new ComponentDescriptor<DetailSchema>(new()
                    {
                        Title = type.Name.Humanize(LetterCasing.Title),
                        Props = [.. type.GetMembers().Properties
                            .Where(p => p.Has<DetailPropertyAttribute>())
                            .Select(p => new DetailSchema.Property{
                                Key = p.GetSingle<DetailPropertyAttribute>().Key,
                                Title = p.GetSingle<DetailPropertyAttribute>().Title,
                                Component = BakedComponents.String
                            })
                        ]
                    })
                    {
                        Data = new RemoteData
                        {
                            Path = type.GetMetadata().GetSingle<DetailAttribute>().Path
                        }
                    };

                    if (type.TryGetMembers(out var members) && members.Methods.Any())
                    {
                        foreach (var method in members.Methods.Where(m => m.Has<TableAttribute>()))
                        {
                            var tableAttribute = method.GetSingle<TableAttribute>();
                            componentDescriptor.Schema.Tables.Add(new ComponentDescriptor<TableSchema>(new()
                            {
                                Title = tableAttribute.Title,
                                Columns = [.. tableAttribute.Columns.Select(c => new TableSchema.Columm { Field = c.ToLowerInvariant(), Header = c })],
                            })
                            {
                                Data = new RemoteData
                                {
                                    Path = tableAttribute.Path
                                }
                            });
                        }
                    }

                    components.Add(type.Name.Pluralize().Kebaberize().ToLowerInvariant(), componentDescriptor);
                }
            });
        });
    }
}