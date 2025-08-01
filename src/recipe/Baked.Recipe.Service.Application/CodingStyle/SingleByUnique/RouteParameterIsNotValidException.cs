using Baked.ExceptionHandling;

namespace Baked.CodingStyle.SingleByUnique;

public class RouteParameterIsNotValidException(string parameter, object? value)
    : HandledException("'{0}' is not a valid {1}", null,
        extraData: new()
        {
            { "value", value },
            { "parameter", parameter }
        }
    );