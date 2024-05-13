using Do.RestApi.Configuration;
using Do.RestApi.Model;

namespace Do.RestApi;

public interface IApiModelConventionCollection : IList<(IApiModelConvention Convention, int Order)>
{
    public void Apply(ApiModel apiModel)
    {
        foreach (var (Convention, _) in this.OrderBy(i => i.Order))
        {
            Console.WriteLine(Convention.GetType().Name);

            if (Convention is IApiModelConvention<ApiModelContext> apiModelConvention)
            {
                apiModelConvention.Apply(new() { Api = apiModel });
            }

            if (Convention is IApiModelConvention<ControllerModelContext> controllerConvention)
            {
                foreach (ControllerModel controller in apiModel.Controllers.ToList())
                {
                    controllerConvention.Apply(new() { Api = apiModel, Controller = controller });
                }
            }

            if (Convention is IApiModelConvention<ActionModelContext> actionConvention)
            {
                foreach (var (controller, action) in apiModel.Controllers
                                                             .SelectMany(c => c.Actions.Select(a => (c, a)))
                                                             .ToList()
                )
                {
                    actionConvention.Apply(new() { Api = apiModel, Controller = controller, Action = action });
                }
            }

            if (Convention is IApiModelConvention<ParameterModelContext> parameterConvention)
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