using System.Net;
using System.Security.Authentication;

namespace Baked.ExceptionHandling.ProblemDetails;

public class AuthenticationExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is AuthenticationException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)HttpStatusCode.Unauthorized,
        "Failed_to_authenticate_with_given_credentials"
    );
}