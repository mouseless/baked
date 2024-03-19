using Do.RestApi.Model;

namespace Do.RestApi;

public class ApiTemplate(ApiModel _apiModel)
{
    internal IEnumerable<string> Render() =>
        _apiModel.Controllers.Select(Render);

    string Render(ControllerModel controller) => $$"""
        namespace RestApi.Generated.Controllers;

        [ApiController]
        public class {{controller.Name}}
        {
            {{ForEach(controller.Actions, Render)}}
        }
    """;

    string Render(ActionModel action) => $$"""
        [Http{{Render(action.Method)}}]
        [Route("{{action.Route}}")]
        public {{Render(action.Return)}} {{action.Name}}({{ForEach(action.Parameters, Render, separator: ", ")}})
        {
            var __target = {{action.Statements.FindTarget}};

            {{If(action.Return.Async && action.Return.Void,
                then: () => "await",
                @else: () => If(action.Return.Async && !action.Return.Void,
                    then: () => "return await",
                    @else: () => If(action.Return.Void,
                        then: () => string.Empty,
                        @else: () => "return"
                    )
                )
            )}} {{Render(action.Statements.InvokeMethod)}};
        }
    """;

    string Render(ReturnModel @return) =>
        @return.Async ? $"async {@return.Type}" :
        @return.Void ? "void" :
        @return.Type
    ;

    string Render(ParameterModel parameter) =>
        $"[From{parameter.From}] {parameter.Type} @{parameter.Name}";

    string Render(InvokeMethodModel invokeMethod) => $"__target.{invokeMethod.Name}();";

    string ForEach<T>(IEnumerable<T> items, Func<T, string> body,
        string? separator = default
    ) => string.Join(separator ?? Environment.NewLine, items.Select(body));

    string If(bool condition, Func<string> then, Func<string>? @else = default) =>
        condition ? then() : @else?.Invoke() ?? string.Empty;

    string Render(HttpMethod method) =>
        $"{method.Method[0]}{method.Method[1..].ToLowerInvariant()}";
}
