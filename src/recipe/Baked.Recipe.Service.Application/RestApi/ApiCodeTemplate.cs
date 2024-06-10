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
        {{If(action.HasBody && action.UseRequestClassForBody, () => $$"""
        public class {{action.Id}}Request
        {
            {{ForEach(action.BodyParameters, Property)}}
        }
        """)}}

        [Http{{Method(action.Method)}}]
        [Route("{{action.GetRoute()}}")]
        {{ForEach(action.AdditionalAttributes, Attribute)}}
        public {{ReturnType(action.Return)}} {{action.Id}}({{Join(", ",
            ForEach(action.ServiceParameters, Parameter, separator: ", "),
            If(action.HasBody, () =>
                If(action.UseRequestClassForBody,
                    () => $$"""[FromBody] {{action.Id}}Request request""",
                @else:
                    () => $$"""[FromBody] {{ParameterWithoutFrom(action.BodyParameters.Single())}}"""
                )
            ),
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
        $"{From(parameter.From)}{ParameterWithoutFrom(parameter)}";

    string ParameterWithoutFrom(ParameterModel parameter) =>
        $"{Attributes(parameter)}{parameter.Type} @{parameter.Name}{If(parameter.IsOptional, () => $" = {parameter.RenderDefaultValue()}")}";

    string Attributes(ParameterModel parameter) =>
        $"{ForEach(parameter.AdditionalAttributes, Attribute)}";

    string From(ParameterModelFrom from) =>
        from != ParameterModelFrom.BodyOrForm
            ? $"[From{from}]"
            : "[FromForm]";

    string Invoke(string target, ActionModel action) => $$"""
        {{(action.Return.IsAsync ? "await " : string.Empty)}}{{target}}.{{action.Id}}(
            {{ForEach(action.InvokedMethodParameters, p => $"@{p.InternalName}: {ParameterLookup(p, action.UseForm, action.UseRequestClassForBody)}", separator: ", ")}}
        )
    """;

    string ParameterLookup(ParameterModel parameter, bool useForm, bool useRequestClassForBody) =>
        $"({parameter.RenderLookup(
            If(useForm || !useRequestClassForBody || !parameter.FromBodyOrForm,
                () => $"@{parameter.Name}",
            @else:
                () => $"request.@{parameter.Name}"
            )
        )})";
}