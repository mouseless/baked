using Do.RestApi.Configuration;

namespace Do.CodingStyle.ObjectAsJson;
public class ObjectParameterFromBodyConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.BodyParameters.Count() != 1) { return; }

        var bodyParameter = context.Action.BodyParameters.First();
        if (bodyParameter.MappedParameter is null) { return; }
        if (!bodyParameter.MappedParameter.ParameterType.Is<object>()) { return; }

        context.Action.UseRequestDto = false;
    }
}