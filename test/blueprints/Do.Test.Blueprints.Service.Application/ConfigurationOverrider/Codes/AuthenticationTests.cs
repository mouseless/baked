namespace Do.Test.ConfigurationOverrider.Codes;

public static class AuthenticationTests
{
    public static readonly string Code = """
        using Do.Authentication;
        using Microsoft.AspNetCore.Mvc;

        namespace Do.Test;

        [ApiController]
        public class AuthenticationController
        {
            [HttpGet]
            [Produces("application/json")]
            [Route("authentication/time")]
            [Use<Authentication.FixedToken.Middleware>]
            public DateTime GetNow([FromServices] AuthenticationTests target)
            {
                var result = target.GetNow();

                return result;
            }

            [HttpPost]
            [Produces("application/json")]
            [Route("authentication/test-form-post-authentication")]
            [Use<Authentication.FixedToken.Middleware>]
            public object TestFormPostAuthentication([FromServices] AuthenticationTests target,
                [FromForm] string value,
                [FromForm] string hash
            )
            {
                return target.TestFormPostAuthentication(value);
            }
        }
    """;
}
