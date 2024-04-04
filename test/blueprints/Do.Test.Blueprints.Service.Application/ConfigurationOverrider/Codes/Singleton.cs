namespace Do.Test.ConfigurationOverrider.Codes;

public static class Singleton
{
    public static readonly string Code = """
        using Do.Authentication;
        using Do.Orm;
        using Microsoft.AspNetCore.Mvc;

        namespace Do.Test;

        [ApiController]
        public class SingletonController
        {
            [HttpGet]
            [Produces("application/json")]
            [Route("singleton/time")]
            public DateTime GetNow([FromServices] Singleton target)
            {
                var result = target.GetNow();

                return result;
            }

            [HttpPost]
            [Produces("application/json")]
            [Route("singleton/test-client")]
            public async Task<object> TestClient([FromServices] Singleton target)
            {
                return await target.TestClient();
            }
        }
    """;
}
