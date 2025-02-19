﻿using Baked.Architecture;
using Baked.Business;
using Baked.UI;
using Humanizer;

namespace Baked.Theme.Admin;

public class AdminThemeFeature(string? _schemaDir) : IFeature<ThemeConfigurator>
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
                        path: $$"""/{{context.Type.Name.Pluralize().Kebaberize().ToLowerInvariant()}}/{0}""")
                    );
                    foreach (var property in context.Type.GetMembers().Properties)
                    {
                        if (property.IsPublic)
                        {
                            property.CustomAttributes.Add(new DetailPropertyAttribute(key: property.Name.Camelize(), title: property.Name));
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
        });

        configurator.ConfigureComponentDescriptors(components =>
        {
            components.SchemaDir = _schemaDir;

            configurator.UsingDomainModel(domain =>
            {
                foreach (var type in domain.Types.Where(t => t.HasMetadata() && t.GetMetadata().Has<DetailAttribute>()))
                {
                    components.Add(type.Name.Pluralize().Kebaberize().ToLowerInvariant(), new ComponentDescriptor<DetailSchema>(new()
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
                    });
                }
            });
        });
    }
}