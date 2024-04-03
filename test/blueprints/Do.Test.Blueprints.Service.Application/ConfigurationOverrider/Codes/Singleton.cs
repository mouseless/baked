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
            [Use<Authentication.FixedToken.Middleware>]
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

            [HttpPost]
            [Produces("application/json")]
            [Route("singleton/test-form-post-authentication")]
            [Use<Authentication.FixedToken.Middleware>]
            public object TestFormPostAuthentication([FromServices] Singleton target,
                [FromForm] string value,
                [FromForm] string hash
            )
            {
                return target.TestFormPostAuthentication(value);
            }

            [HttpPost]
            [Produces("application/json")]
            [Route("singleton/test-exception")]
            public void TestException([FromServices] Singleton target, bool handled)
            {
                target.TestException(handled);
            }
        }
    """;
}
