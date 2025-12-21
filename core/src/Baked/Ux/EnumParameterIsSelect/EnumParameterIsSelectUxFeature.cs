using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;

namespace Baked.Ux.EnumParameterIsSelect;

public class EnumParameterIsSelectUxFeature(int _maxMemberCountForSelectButton)
    : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Use `SelectButton` when enum member count is <= _maxMemberCountForSelectButton
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => ParameterSelectButton(c.Parameter, cc),
                when: c =>
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

            // Use `Select` when enum member count is > _maxMemberCountForSelectButton
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => ParameterSelect(c.Parameter, cc),
                when: c =>
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
            builder.Conventions.AddParameterSchemaConfiguration<Input>(
                schema: (p, c, cc) => p.DefaultValue = c.Parameter.ParameterType.SkipNullable().GetEnumNames().First(),
                when: c =>
                    c.Parameter.ParameterType.SkipNullable().IsEnum &&
                    c.Parameter.TryGet<ParameterModelAttribute>(out var api) &&
                    !api.IsOptional,
                order: 10
            );
        });
    }
}