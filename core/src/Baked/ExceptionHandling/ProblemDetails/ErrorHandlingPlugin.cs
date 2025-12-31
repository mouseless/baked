using Baked.Ui;
using Baked.Ui.Configuration;

namespace Baked.ExceptionHandling.ProblemDetails;

public record ErrorHandlingPlugin : ModulePluginBase
{
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