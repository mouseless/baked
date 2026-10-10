using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.ComponentModel.DataAnnotations;

namespace Baked.CodingStyle.UseNullableTypes;

public class RequiredParametersAreRequiredInApiModelConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGet<ApiParameter>(out var parameter)) { return; }
        if (!context.Parameter.Has<RequiredAttribute>()) { return; }

        parameter.AddRequiredAttributes(isValueType: context.Parameter.ParameterType.IsValueType);
    }
}