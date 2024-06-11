using Baked.ExceptionHandling;

namespace Baked.CodingStyle.SingleByUnique;

public class RouteParameterIsNotValidException(string parameter, object? value)
    : HandledException($"'{value}' is not a valid {parameter}", null,
        extraData: new() { { "parameter", parameter }, { "value", value } }
    )
{ }