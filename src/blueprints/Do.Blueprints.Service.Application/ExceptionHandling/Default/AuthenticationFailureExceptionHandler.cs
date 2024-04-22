
using Microsoft.AspNetCore.Authentication;
using System.Net;

namespace Do.ExceptionHandling.Default;

public class AuthenticationFailureExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is AuthenticationFailureException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)HttpStatusCode.Unauthorized, ex.Message);
}