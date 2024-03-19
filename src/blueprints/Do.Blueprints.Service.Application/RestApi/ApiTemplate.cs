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
            {{ForEach(controller.Actions, action => $$"""
            [HttpGet]
            [Route("{{controller.Name}}/{{action.Name}}")]
            public void {{action.Name}}()
            {
            }
            """)}}
        }
    """;

    string ForEach<T>(IEnumerable<T> items, Func<T, string> body) =>
        string.Join(Environment.NewLine, items.Select(body));
}
