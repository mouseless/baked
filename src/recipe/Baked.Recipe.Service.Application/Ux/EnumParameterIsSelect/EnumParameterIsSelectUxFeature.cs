using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Theme.Admin;

using static Baked.Theme.Admin.DomainComponents;

namespace Baked.Ux.EnumParameterIsSelect;

public class EnumParameterIsSelectUxFeature(int _maxMemberCountForSelectButton)
    : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Use `SelectButton` when enun member count is <= _maxMemberCountForSelectButton
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelectButton(c.Parameter, cc),
                whenParameter: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() <= _maxMemberCountForSelectButton
            );
            builder.Conventions.AddParameterComponentConfiguration<SelectButton>(
                component: (s, c) =>
                {
                    var api = c.Parameter.Get<ParameterModelAttribute>();

                    s.Schema.AllowEmpty = api.IsOptional && api.DefaultValue is null ? true : null;
                    if (s.Data?.RequireLocalization == true)
                    {
                        s.Schema.OptionLabel = "label";
                        s.Schema.OptionValue = "value";
                    }
                }
            );

            // Use `Select` when enun member count is > _maxMemberCountForSelectButton
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                whenParameter: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.ParameterType.SkipNullable().GetEnumNames().Count() > _maxMemberCountForSelectButton
            );
            builder.Conventions.AddParameterComponentConfiguration<Select>(
                component: (s, c) =>
                {
                    var api = c.Parameter.Get<ParameterModelAttribute>();

                    s.Schema.ShowClear = api.IsOptional && api.DefaultValue is null ? true : null;
                    if (s.Data?.RequireLocalization == true)
                    {
                        s.Schema.OptionLabel = "label";
                        s.Schema.OptionValue = "value";
                    }
                }
            );

            // Default value of a required enum parameter is set to the first enum member
            builder.Conventions.AddParameterSchemaConfiguration<Parameter>(
                schema: (p, c, cc) => p.DefaultValue = c.Parameter.ParameterType.SkipNullable().GetEnumNames().First(),
                whenParameter: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.TryGet<ParameterModelAttribute>(out var api) &&
                    !api.IsOptional,
                order: 10
            );
        });
    }
}