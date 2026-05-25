using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Ui;
using System.ComponentModel.DataAnnotations;

using static Baked.Theme.Default.DomainComponents;

namespace Baked.Ux.EnumParameterIsSelect;

public class EnumParameterIsSelectUxFeature(int _maxMemberCountForSelectButton)
    : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            // Use `SelectButton` when enum member count is <= _maxMemberCountForSelectButton
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() <= _maxMemberCountForSelectButton,
                component: (c, cc) => ParameterSelectButton(c.Parameter, cc)
            );
            conventions.AddParameterComponentConfiguration<SelectButton>(
                when: c => c.Parameter.ParameterType.SkipNullable().IsEnum,
                component: (s, c) =>
                {
                    if (s.Data?.RequireLocalization == true)
                    {
                        s.Schema.OptionLabel = "label";
                        s.Schema.OptionValue = "value";
                    }
                }
            );

            // Use `Select` when enum member count is > _maxMemberCountForSelectButton
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() > _maxMemberCountForSelectButton,
                component: (c, cc) => ParameterSelect(c.Parameter, cc)
            );
            conventions.AddParameterComponentConfiguration<Select>(
                when: c => c.Parameter.ParameterType.SkipNullable().IsEnum,
                component: (s, c) =>
                {
                    if (s.Data?.RequireLocalization == true)
                    {
                        s.Schema.OptionLabel = "label";
                        s.Schema.OptionValue = "value";
                    }
                }
            );

            // Default value of a required enum parameter is set to the first enum
            // member when it is in query or route
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.Has<RequiredAttribute>() &&
                    c.Parameter.TryGet<ParameterModelAttribute>(out var api) &&
                    (api.FromQuery || api.FromRoute),
                schema: (p, c, cc) => p.DefaultValue = c.Parameter.ParameterType.SkipNullable().GetEnumNames().First(),
                order: 10
            );
        });
    }
}