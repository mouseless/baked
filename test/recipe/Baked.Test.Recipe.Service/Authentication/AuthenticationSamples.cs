﻿using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Baked.Test.Authentication;

public class AuthenticationSamples(
    Func<ClaimsPrincipal> _getClaims,
    ILogger<AuthenticationSamples> _logger
)
{
    public string? Authenticate() =>
        _getClaims().Identity?.AuthenticationType;

    public string? FormPostAuthenticate(string value)
    {
        _logger.LogInformation($"Form post authenticate is called with value:'{value}'");

        return _getClaims().Identity?.AuthenticationType;
    }
}