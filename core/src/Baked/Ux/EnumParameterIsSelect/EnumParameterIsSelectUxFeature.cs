using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Ui;
using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace Baked;

public static class EnumParameterIsSelectUxFeature
{
    public static IFeature<UxFeature.Configurator> EnumParameterIsSelect(this UxFeature.Configurator x,
        int maxMemberCountForSelectButton = 3
    ) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            // Use `SelectButton` when enum member count is <= maxMemberCountForSelectButton
            conventions.AddParameterComponent<SelectButton>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() <= maxMemberCountForSelectButton
            );

            // Use `Select` when enum member count is > maxMemberCountForSelectButton
            conventions.AddParameterComponent<Select>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() > maxMemberCountForSelectButton
            );

            // Use `MultiSelectButton` for flags enum, when enum member count is <= maxMemberCountForSelectButton
            conventions.AddParameterComponent<MultiSelectButton>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() <= maxMemberCountForSelectButton &&
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<FlagsAttribute>()
            );

            // Use `MultiSelect` for flags enum, when enum member count is > maxMemberCountForSelectButton
            conventions.AddParameterComponent<MultiSelect>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() > maxMemberCountForSelectButton &&
                    c.Parameter.ParameterType.SkipNullable().TryGetMetadata(out var metadata) && metadata.Has<FlagsAttribute>()
            );

            // Default value of a required enum parameter is set to the first enum
            // member (camelCase) when it is in query or route
            conventions.EditParameterSchema<Input>(
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.Has<RequiredAttribute>() &&
                    c.Parameter.TryGet<ApiParameter>(out var api) &&
                    (api.FromQuery || api.FromRoute),
                schema: (p, c) => p.DefaultValue = c.Parameter.ParameterType.SkipNullable().GetEnumNames().First().Camelize(),
                order: 20
            );

            // Map option label and value for enum data
            conventions.EditParameterSchema<Input>(
                when: c => c.Parameter.ParameterType.SkipNullable().IsEnum,
                schema: i =>
                {
                    if (i.Component.Schema is not ISelect select) { return; }

                    select.OptionLabel = "label";
                    select.OptionValue = "value";
                }
            );

            // Use localize option labels for flags enum
            conventions.EditParameterSchema<Input>(
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
    });
}