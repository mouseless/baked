using Baked.Ui;

namespace Baked.Theme.Admin;

public class ErrorHandlingOptions : INamedSettings
{
    public DefaultHandlerOptions DefaultHandler { get; init; } = new();

    string INamedSettings.Name => "errorHandling";

    public class DefaultHandlerOptions
    {
        public List<HandlerOption> Config { get; init; } = [];

        public record HandlerOption(
            string? RoutePattern = default,
            int? StatusCode = default,
            string? Result = default
        );
    }
}