using Baked.Domain.Model;
using Baked.RestApi.Model;
using Baked.Theme.Admin;
using Baked.Ui;
using Humanizer;

using static Baked.Test.Theme.Custom.DomainDatas;
using static Baked.Theme.Admin.Components;

namespace Baked.Test.Theme.Custom;

public static class DomainComponents
{
    public static ComponentDescriptorAttribute<Baked.Theme.Admin.String> ActionString(MethodModel method) =>
        String(data: ActionRemote(method));

    public static ComponentDescriptorAttribute<Select> EnumSelect(string label, TypeModel enumType,
        Action<Select>? options = default
    ) => Select(label, EnumInline(enumType),
        options: options
    );

    public static Parameter EnumSelectParameter(ParameterModel parameter, ComponentContext context) =>
        ParameterParameter(parameter,
            component: p => EnumSelect(context.NewLocaleKey(p.Name.Titleize()), p.ParameterType),
            options: p =>
            {
                p.Required = true;
                p.DefaultValue = parameter.ParameterType.GetEnumNames().First();
            }
        );

    public static Parameter ParameterParameter(ParameterModel parameter, Func<ParameterModel, IComponentDescriptor> component,
        Action<Parameter>? options = default
    )
    {
        var api = parameter.GetSingle<ParameterModelAttribute>();

        return Parameter(api.Name, component(parameter), options: p =>
        {
            p.Required = !api.IsOptional ? true : null;

            options.Apply(p);
        });
    }
}