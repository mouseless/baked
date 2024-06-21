using Baked.Domain.Model;
using Baked.RestApi.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authentication.FixedBearerToken;

public class AddParameterToFormPostConvention(DomainModel _domain, string _name, string _description)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Action.UseForm) { return; }
        if (context.Action.Parameter.ContainsKey(_name)) { return; }

        context.Action.Parameter.Add(
            _name,
            new(_domain.Types[typeof(string)], ParameterModelFrom.BodyOrForm, _name)
            {
                IsInvokeMethodParameter = false,
                AdditionalAttributes = [$"SwaggerSchema(\"{_description}\")"]
            }
        );
    }
}