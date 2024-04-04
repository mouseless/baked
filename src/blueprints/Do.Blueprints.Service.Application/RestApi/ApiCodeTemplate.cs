using Do.CodeGeneration;
using Do.RestApi.Model;

namespace Do.RestApi;

public class ApiCodeTemplate(ApiModel _apiModel)
    : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        _apiModel.Controllers.Select(Controller);

    string Controller(ControllerModel controller) => $$"""
        namespace RestApiLayer;

        [ApiController]
        public class {{controller.ClassName}}
        {
            {{ForEach(controller.Actions, Action)}}
        }
    """;

    string Action(ActionModel action) => $$"""
        {{If(action.HasRequestBody, () => $$"""
        public record {{action.Name}}Request(
            {{ForEach(action.BodyParameters, parameter => $"{parameter.Type} @{parameter.Name}", separator: ", ")}}
        );
        """)}}

        [Http{{Method(action.Method)}}]
        [Route("{{action.RouteStylized}}")]
        public {{ReturnType(action.Return)}} {{action.Name}}(
            {{ForEach(action.NonBodyParameters, Parameter, separator: ", ")}}
            {{If(action.HasRequestBody, () => $$"""
            , [FromBody] {{action.Name}}Request request
            """)}}
        )
        {
            var __target = {{action.FindTargetStatement}};

            {{Return(action.Return)}} __target.{{action.InvokedMethodName}}(
                {{ForEach(action.InvokedMethodParameters, p => $"@{p.InternalName}: {ParameterLookup(p)}", separator: ", ")}}
            );
        }
    """;

    string Method(HttpMethod method) =>
        $"{method.Method[0]}{method.Method[1..].ToLowerInvariant()}";

    string ReturnType(ReturnModel @return) =>
        @return.IsAsync ? $"async {@return.Type}" :
        @return.Type
    ;

    string Parameter(ParameterModel parameter) =>
        $"[From{parameter.From}] {parameter.Type} @{parameter.Name}";

    string ParameterLookup(ParameterModel parameter) =>
        $"({parameter.RenderLookup(parameter.FromBody ? $"request.@{parameter.Name}" : $"@{parameter.Name}")})";

    string Return(ReturnModel @return) =>
        @return.IsAsync && @return.IsVoid ? "await" :
        @return.IsAsync && !@return.IsVoid ? "return await" :
        @return.IsVoid ? string.Empty :
        "return";
}
