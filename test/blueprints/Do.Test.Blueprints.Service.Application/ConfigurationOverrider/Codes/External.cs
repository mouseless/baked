namespace Do.Test.ConfigurationOverrider.Codes;

public static class External
{
    public static readonly string Code = """
        using Microsoft.AspNetCore.Mvc;

        namespace Do.Test;

        [ApiController]
        public class ExternalController
        {
            [HttpPost]
            [Produces("application/json")]
            [Route("external/test-client")]
            public async Task<object> TestClient([FromServices] External target)
            {
                return await target.TestClient();
            }
        }
    """;
}
