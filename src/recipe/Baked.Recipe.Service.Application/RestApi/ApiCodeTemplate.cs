using Baked.CodeGeneration;
using Baked.RestApi.Model;

namespace Baked.RestApi;

public class ApiCodeTemplate(ApiModel _apiModel)
    : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        _apiModel.Controllers.Select(Controller);

    string Controller(ControllerModelAttribute controller) => $$"""
        namespace RestApiLayer;

        [ApiController]
        [ApiExplorerSettings(GroupName = "{{controller.GroupName}}")]
        public class {{controller.ClassName}}
        {
            {{ForEach(controller.Actions, Action)}}
        }
    """;

    string Action(ActionModelAttribute action) => $$"""
        {{If(action.UseForm || action.HasBody && action.UseRequestClassForBody, () => $$"""
        public class {{action.Id}}Request
        {
            {{ForEach(action.BodyParameters, Property)}}
        }
        """)}}

        [Http{{Method(action.Method)}}]
        [Route("{{action.GetRoute()}}")]
        {{ForEach(action.AdditionalAttributes, Attribute)}}
        public {{ReturnType(action)}} {{action.Id}}({{Join(", ",
            ForEach(action.ServiceParameters, Parameter, separator: ", "),
            If(action.UseForm || action.HasBody, () =>
                If(action.UseForm,
                    () => $$"""[FromForm] {{action.Id}}Request request""",
                @else: () =>
                    If(action.UseRequestClassForBody,
                        () => $$"""[FromBody] {{action.Id}}Request request""",
                    @else:
                        () => $$"""[FromBody] {{ParameterWithoutFrom(action.BodyParameters.Single())}}"""
                    )
                )
            ),
            ForEach(action.NonBodyParameters, Parameter, separator: ", ")
        )}})
        {
            {{ForEach(action.PreparationStatements, statement => $$"""
                {{statement}}
            """)}}
            var __target = {{action.FindTargetStatement}};

            {{If(action.ReturnIsVoid, () => $$"""
                {{Invoke("__target", action)}};
            """, @else: () => $$"""
                var __result = {{Invoke("__target", action)}};

                return {{action.RenderReturnResult("__result")}};
            """)}}
        }
    """;

    string Property(ParameterModelAttribute parameter) => $$"""
        {{Attributes(parameter)}}
        public {{parameter.Type}} @{{parameter.Name}} { get; set; }{{If(parameter.IsOptional, () => $" = {parameter.RenderDefaultValue()};")}}
    """;

    string Method(HttpMethod method) =>
        $"{method.Method[0]}{method.Method[1..].ToLowerInvariant()}";

    string Attribute(string attribute) =>
        $"[{attribute}]";

    string ReturnType(ActionModelAttribute action) =>
        action.ReturnIsAsync ? $"async {action.ReturnType}" :
        action.ReturnType
    ;

    string Parameter(ParameterModelAttribute parameter) =>
        $"{From(parameter.From)}{ParameterWithoutFrom(parameter)}";

    string ParameterWithoutFrom(ParameterModelAttribute parameter) =>
        $"{Attributes(parameter)}{parameter.Type} @{parameter.Name}{If(parameter.IsOptional, () => $" = {parameter.RenderDefaultValue()}")}";

    string Attributes(ParameterModelAttribute parameter) =>
        $"{ForEach(parameter.AdditionalAttributes, Attribute)}";

    string From(ParameterModelFrom from) =>
        from != ParameterModelFrom.BodyOrForm
            ? $"[From{from}]"
            : "[FromForm]";

    string Invoke(string target, ActionModelAttribute action) => $$"""
        {{(action.InvocationIsAsync ? "await " : string.Empty)}}{{target}}.{{action.Id}}(
            {{ForEach(action.InvokedMethodParameters, p => $"@{p.InternalName}: {ParameterLookup(p, action.UseForm, action.UseRequestClassForBody)}", separator: ", ")}}
        )
    """;

    string ParameterLookup(ParameterModelAttribute parameter, bool useForm, bool useRequestClassForBody) =>
        $"({parameter.RenderLookup(
            If(useForm || useRequestClassForBody && parameter.FromBodyOrForm,
                () => $"request.@{parameter.Name}",
            @else:
                () => $"@{parameter.Name}"
            )
        )})";
}