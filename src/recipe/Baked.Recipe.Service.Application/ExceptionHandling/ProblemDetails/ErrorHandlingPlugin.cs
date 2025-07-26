using Baked.Ui;

namespace Baked.ExceptionHandling.ProblemDetails;

public record ErrorHandlingPlugin : PluginBase
{
    public List<Handler> Handlers { get; init; } = [];
    public string DefaultAlertTitle { get; init; } = "Unexpected_Error";
    public string DefaultAlertMessage { get; init; } = "Please_contact_system_administrator";

    public record Handler(
        string? RoutePattern = default,
        int? StatusCode = default,
        HandlerBehavior Behavior = default,
        IData? BehaviorArgument = default,
        int Order = 0
    );

    public enum HandlerBehavior
    {
        None = 0,
        Alert = 1,
        Page = 2,
        Redirect = 3
    }
}