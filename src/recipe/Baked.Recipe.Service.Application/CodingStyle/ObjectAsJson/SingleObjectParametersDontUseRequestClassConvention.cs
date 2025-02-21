namespace Baked.CodingStyle.ObjectAsJson;

public class SingleObjectParametersDontUseRequestClassConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.BodyParameters.Count() != 1) { return; }

        var bodyParameter = context.Action.BodyParameters.Single();
        if (bodyParameter.MappedParameter is null) { return; }
        if (!bodyParameter.MappedParameter.ParameterType.Is<object>()) { return; }

        context.Action.UseRequestClassForBody = false;
    }
}