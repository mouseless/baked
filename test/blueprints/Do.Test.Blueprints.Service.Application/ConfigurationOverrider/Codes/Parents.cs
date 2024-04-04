namespace Do.Test.ConfigurationOverrider.Codes;

public static class Parents
{
    public static readonly string Code = """
        using Do.Orm;
        using Microsoft.AspNetCore.Mvc;

        namespace Do.Test.RestApi.Analyzer;

        [ApiController]
        public class ParentsController
        {
            public record ByRequest(
                string Name = default,
                bool Asc = false,
                bool Desc = false,
                int? Take = default,
                int? Skip = default
            );

            [HttpGet]
            [Route("parents")]
            public List<Parent> By([FromServices] Parents target, [FromQuery] ByRequest request)
            {
                return target.By(
                    name: request.Name,
                    asc: request.Asc,
                    desc: request.Desc,
                    take: request.Take,
                    skip: request.Skip
                );
            }

            public record ByNameRequest(string Contains,
                bool Asc = false,
                bool Desc = false,
                int? Take = default,
                int? Skip = default
            );

            [HttpGet]
            [Route("parents/by-name")]
            public List<Parent> ByName([FromServices] Parents target, [FromQuery] ByNameRequest request)
            {
                return target.ByName(request.Contains,
                    asc: request.Asc,
                    desc: request.Desc,
                    take: request.Take,
                    skip: request.Skip
                );
            }

            [HttpGet]
            [Route("parents/{id}/children")]
            public List<Child> GetChildren([FromServices] IQueryContext<Parent> parentQuery, [FromRoute] Guid id)
            {
                var target = parentQuery.SingleById(id);

                return target.GetChildren();
            }

            public record AddChildRequest();

            [HttpPost]
            [Route("parents/{id}/children")]
            public void AddChild([FromServices] IQueryContext<Parent> parentQuery, [FromRoute] Guid id, [FromBody] AddChildRequest request)
            {
                var target = parentQuery.SingleById(id);

                target.AddChild();
            }
        }
    """;
}
