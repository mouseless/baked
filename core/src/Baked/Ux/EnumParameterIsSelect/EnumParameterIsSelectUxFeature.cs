using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Ui;
using Humanizer;
using System.ComponentModel.DataAnnotations;

using B = Baked.Ui.Components;

namespace Baked.Ux.EnumParameterIsSelect;

public class EnumParameterIsSelectUxFeature(int _maxMemberCountForSelectButton)
    : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            // Use `SelectButton` when enum member count is <= _maxMemberCountForSelectButton
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() <= _maxMemberCountForSelectButton,
                component: () => B.SelectButton()
            );

            // Use `Select` when enum member count is > _maxMemberCountForSelectButton
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() > _maxMemberCountForSelectButton,
                component: () => B.Select()
            );

            // Use `MultiSelectButton` for flags enum, when enum member count is <= _maxMemberCountForSelectButton
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() <= _maxMemberCountForSelectButton &&
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<FlagsAttribute>(),
                component: () => B.MultiSelectButton()
            );

            // Use `MultiSelect` for flags enum, when enum member count is > _maxMemberCountForSelectButton
            conventions.AddParameterComponent(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() > _maxMemberCountForSelectButton &&
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<FlagsAttribute>(),
                component: () => B.MultiSelect()
            );

            // Default value of a required enum parameter is set to the first enum
            // member (camelCase) when it is in query or route
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.Has<RequiredAttribute>() &&
                    c.Parameter.TryGet<ParameterModelAttribute>(out var api) &&
                    (api.FromQuery || api.FromRoute),
                schema: (p, c, cc) => p.DefaultValue = c.Parameter.ParameterType.SkipNullable().GetEnumNames().First().Camelize(),
                order: 10
            );

            // Map option label and value for enum data
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c => c.Parameter.ParameterType.SkipNullable().IsEnum,
                schema: i =>
                {
                    if (i.Component.Schema is not ISelect select) { return; }

                    select.OptionLabel = "label";
                    select.OptionValue = "value";
                }
            );

            // Use localize option labels for flags enum
            conventions.AddParameterSchemaConfiguration<Input>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<FlagsAttribute>(),
                schema: (i, _, cc) =>
                {
                    if (i.Component.Schema is not ISelect select) { return; }

                    var (_, l) = cc;

                    select.LocalizeOptionLabels = true;
                }
            );
        });
    }
}