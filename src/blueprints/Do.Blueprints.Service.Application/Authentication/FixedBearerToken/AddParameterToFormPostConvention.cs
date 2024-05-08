using Do.Domain.Model;
using Do.RestApi.Configuration;
using Do.RestApi.Model;

namespace Do.Authentication.FixedBearerToken;

public class AddParameterToFormPostConvention(DomainModel _domain, string _name)
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
                IsInvokeMethodParameter = false
            }
        );
    }
}
