using Baked.Ui;
using Humanizer;

namespace Baked.ExceptionHandling.ProblemDetails;

public class ErrorHandlingPlugin : IPlugin
{
    public string Name => nameof(ErrorHandlingPlugin).Replace("Plugin", string.Empty).Camelize();
    public List<Handler> Handlers { get; init; } = [];
    public string DefaultAlertTitle { get; init; } = "Unexpected Error";
    public string DefaultAlertMessage { get; init; } = "Please contact system administrator";

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