using Do.RestApi.Configuration;
using Do.RestApi.Model;

namespace Do.RestApi;

public interface IApiModelConventionCollection : IList<IApiModelConvention>
{
    public void Apply(ApiModel apiModel)
    {
        var apiModelContext = new ApiModelContext { Api = apiModel };
        foreach (var apiModelConvention in this.OfType<IApiModelConvention<ApiModelContext>>())
        {
            apiModelConvention.Apply(apiModelContext);
        }

        foreach (var controllerConvention in this.OfType<IApiModelConvention<ControllerModelContext>>())
        {
            foreach (ControllerModel controller in apiModel.Controllers.ToList())
            {
                controllerConvention.Apply(new() { Api = apiModel, Controller = controller });
            }
        }

        foreach (var actionConvention in this.OfType<IApiModelConvention<ActionModelContext>>())
        {
            foreach (var (controller, action) in apiModel.Controllers
                                                         .SelectMany(c => c.Actions.Select(a => (c, a)))
                                                         .ToList()
            )
            {
                actionConvention.Apply(new() { Api = apiModel, Controller = controller, Action = action });
            }
        }

        foreach (var parameterConvention in this.OfType<IApiModelConvention<ParameterModelContext>>())
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
