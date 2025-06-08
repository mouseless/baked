using Baked.ExceptionHandling;

namespace Baked.CodingStyle.SingleByUnique;

public class RouteParameterIsNotValidException(string parameter, object? value)
    : HandledException("VALUE_is_not_a_valid_PARAMETER", null,
        extraData: new()
        {
            { "value", value == null ? "route" : value.ToString()! },
            { "parameter", parameter }
        }
    );