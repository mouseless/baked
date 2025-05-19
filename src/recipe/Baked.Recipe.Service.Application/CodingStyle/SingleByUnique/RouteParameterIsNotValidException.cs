using Baked.ExceptionHandling;

namespace Baked.CodingStyle.SingleByUnique;

public class RouteParameterIsNotValidException(string parameter, object? value)
    : HandledException($"'{value}' is not a valid {parameter}", null,
        extraData: new() { { "parameter", parameter }, { "value", value } }
    )
{
    public override string LKey => "VALUE_is_not_a_valid_PARAMETER";
    public override string[] LParams => [value == null ? "route" : value.ToString()!, parameter];
}