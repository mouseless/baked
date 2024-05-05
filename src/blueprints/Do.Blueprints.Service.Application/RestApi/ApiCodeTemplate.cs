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
        [ApiExplorerSettings(GroupName = "{{controller.GroupName}}")]
        public class {{controller.ClassName}}
        {
            {{ForEach(controller.Actions, Action)}}
        }
    """;

    string Action(ActionModel action) => $$"""
        {{If(action.HasBody, () => $$"""
        public class {{action.Id}}Request
        {
            {{ForEach(action.BodyParameters, Property)}}
        }

        """)}}

        [Http{{Method(action.Method)}}]
        [Route("{{action.RouteStylized}}")]
        {{ForEach(action.AdditionalAttributes, Attribute)}}
        public {{ReturnType(action.Return)}} {{action.Id}}({{Join(", ",
            ForEach(action.ServiceParameters, Parameter, separator: ", "),
            If(action.HasBody, () => $$"""[FromBody] {{action.Id}}Request request"""),
            ForEach(action.NonBodyParameters, Parameter, separator: ", ")
        )}})
        {
            {{ForEach(action.PreparationStatements, statement => $$"""
                {{statement}}
            """)}}
            var __target = {{action.FindTargetStatement}};

            {{If(action.Return.IsVoid, () => $$"""
                {{Invoke("__target", action)}};
            """, @else: () => $$"""
                var __result = {{Invoke("__target", action)}};

                return {{action.Return.RenderResult("__result")}};
            """)}}
        }
    """;

    string Property(ParameterModel parameter) => $$"""
        {{Attributes(parameter)}}
        public {{parameter.Type}} @{{parameter.Name}} { get; set; }{{If(parameter.IsOptional, () => $" = {parameter.RenderDefaultValue()};")}}
    """;

    string Method(HttpMethod method) =>
        $"{method.Method[0]}{method.Method[1..].ToLowerInvariant()}";

    string Attribute(string attribute) =>
        $"[{attribute}]";

    string ReturnType(ReturnModel @return) =>
        @return.IsAsync ? $"async {@return.Type}" :
        @return.Type
    ;

    string Parameter(ParameterModel parameter) =>
        $"{Attributes(parameter)}{parameter.Type} @{parameter.Name}{If(parameter.IsOptional, () => $" = {parameter.RenderDefaultValue()}")}";

    string Attributes(ParameterModel parameter) =>
        $"{ParameterFrom(parameter.From)}{ForEach(parameter.AdditionalAttributes, Attribute)}";

    string ParameterFrom(ParameterModelFrom parameterFrom) =>
        parameterFrom != ParameterModelFrom.BodyOrForm
            ? $"[From{parameterFrom}]"
            : "[FromForm]";

    string Invoke(string target, ActionModel action) => $$"""
        {{(action.Return.IsAsync ? "await " : string.Empty)}}{{target}}.{{action.Id}}(
            {{ForEach(action.InvokedMethodParameters, p => $"@{p.InternalName}: {ParameterLookup(p, action.UseForm)}", separator: ", ")}}
        )
    """;

    string ParameterLookup(ParameterModel parameter, bool useForm) =>
        $"({parameter.RenderLookup(
            If(useForm || !parameter.FromBodyOrForm,
                () => $"@{parameter.Name}",
            @else:
                () => $"request.@{parameter.Name}"
            )
        )})";
}