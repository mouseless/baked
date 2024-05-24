using Do.RestApi.Configuration;

namespace Do.CodingStyle.ObjectAsJson;
public class ObjectParameterFromBodyConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.BodyParameters.Count() != 1) { return; }
        if (!context.Action.BodyParameters.First().MappedParameter?.ParameterType.Is<object>() == true) { return; }

        context.Action.UseRequestDto = false;
    }
}
