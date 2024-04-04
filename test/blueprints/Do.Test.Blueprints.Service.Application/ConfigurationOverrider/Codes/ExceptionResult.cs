namespace Do.Test.ConfigurationOverrider.Codes;

public static class ExceptionResult
{
    public static readonly string Code = """
        using Microsoft.AspNetCore.Mvc;

        namespace Do.Test;

        [ApiController]
        public class ExceptionResultController
        {
            [HttpPost]
            [Produces("application/json")]
            [Route("exception-result/throw")]
            public void TestException([FromServices] ExceptionResult target, bool handled)
            {
                target.Throw(handled);
            }
        }
    """;
}
