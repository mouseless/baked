using Do.CodeGeneration;
using Do.RestApi.Model;

namespace Do.RestApi;

public class ApiCodeTemplate(ApiModel _apiModel)
    : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        _apiModel.Controllers.Select(Controller);

    string Controller(ControllerModel controller) => $$"""
        namespace RestApi.Generated.Controllers;

        [ApiController]
        public class {{controller.Name}}
        {
            {{ForEach(controller.Actions, Action)}}
        }
    """;

    string Action(ActionModel action) => $$"""
        [Http{{Method(action.Method)}}]
        [Route("{{action.Route}}")]
        public {{ReturnType(action.Return)}} {{action.Name}}({{ForEach(action.Parameters, Parameter, separator: ", ")}})
        {
            var __target = {{action.Statements.FindTarget}};

            {{Return(action.Return)}} __target.{{action.Statements.InvokeMethod.Name}}();
        }
    """;

    string Method(HttpMethod method) =>
        $"{method.Method[0]}{method.Method[1..].ToLowerInvariant()}";

    string ReturnType(ReturnModel @return) =>
        @return.Async ? $"async {@return.Type}" :
        @return.Void ? "void" :
        @return.Type
    ;

    string Parameter(ParameterModel parameter) =>
        $"[From{parameter.From}] {parameter.Type} @{parameter.Name}";

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    string InvokeMethod(InvokeMethodModel invokeMethod) => $"__target.{invokeMethod.Name}();";

    string Return(ReturnModel @return) =>
        @return.Async && @return.Void ? "await" :
        @return.Async && !@return.Void ? "return await" :
        @return.Void ? string.Empty :
        "return";
}
