using Do.RestApi.Configuration;
using Do.RestApi.Model;

namespace Do.Business.Default.RestApiConventions;

public class GetAndDeleteAcceptsQueryConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.Method != HttpMethod.Get && context.Action.Method != HttpMethod.Delete) { return; }
        if (!context.Parameter.FromBody && !context.Parameter.FromForm) { return; }

        context.Parameter.From = ParameterModelFrom.Query;
    }
}
