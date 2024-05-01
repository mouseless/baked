using Do.ExceptionHandling;

namespace Do.CodingStyle.SingleByUnique;

public class RouteParameterIsNotValidException(string parameter, object? value)
    : HandledException($"{parameter} is not valid: '{value}'", null,
        extraData: new() { { "parameter", parameter }, { "value", value } }
    )
{ }