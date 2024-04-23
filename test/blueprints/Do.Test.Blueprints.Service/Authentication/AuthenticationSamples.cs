using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Do.Test.Authentication;

public class AuthenticationSamples(
    ILogger<AuthenticationSamples> _logger,
    Func<ClaimsPrincipal> _getClaims
)
{
    public void TokenAuthentication()
    {
        _logger.LogInformation($"User:{JsonConvert.SerializeObject(_getClaims())}");
    }

    public object FormPostAuthentication(object value)
    {
        _logger.LogInformation($"User:{JsonConvert.SerializeObject(_getClaims())}");

        return value;
    }
}