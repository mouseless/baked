using Do.RestApi.Configuration;
using Do.RestApi.Model;

namespace Do.RestApi;

public interface IApiModelConventionCollection : IList<IApiModelConvention>
{
    public new void Add(IApiModelConvention convention)
    {
        int index = ((Count % 2) == 0) ? (Count / 2) : (Count + 1) / 2;
        Insert(index, convention);
    }

    public void Apply(ApiModel apiModel)
    {
        foreach (var convention in this)
        {
            if (convention is IApiModelConvention<ApiModelContext> apiModelConvention)
            {
                apiModelConvention.Apply(new() { Api = apiModel });
            }

            if (convention is IApiModelConvention<ControllerModelContext> controllerConvention)
            {
                foreach (ControllerModel controller in apiModel.Controllers.ToList())
                {
                    controllerConvention.Apply(new() { Api = apiModel, Controller = controller });
                }
            }

            if (convention is IApiModelConvention<ActionModelContext> actionConvention)
            {
                foreach (var (controller, action) in apiModel.Controllers
                                                             .SelectMany(c => c.Actions.Select(a => (c, a)))
                                                             .ToList()
                )
                {
                    actionConvention.Apply(new() { Api = apiModel, Controller = controller, Action = action });
                }
            }

            if (convention is IApiModelConvention<ParameterModelContext> parameterConvention)
            {
                foreach (var (controller, action, parameter) in apiModel.Controllers
                                                                        .SelectMany(c => c.Actions.Select(a => (c, a)))
                                                                        .SelectMany(x => x.a.Parameters.Select(p => (x.c, x.a, p)))
                                                                        .ToList()
                )
                {
                    parameterConvention.Apply(new() { Api = apiModel, Controller = controller, Action = action, Parameter = parameter });
                }
            }
        }
    }
}