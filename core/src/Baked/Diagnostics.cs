using Spectre.Console;

namespace Baked;

public class Diagnostics : IDisposable
{
    static Diagnostics? _current;

    public static Diagnostics Current
    {
        get
        {
            if (_current is null)
            {
                throw new InvalidOperationException("There is no active diagnostics. Start one using `Diagnostics.Start(...)`");
            }

            return _current;
        }
    }

    public static Diagnostics Start(string name,
        Action<DiagnosticsResult>? onDispose = default
    )
    {
        if (_current is not null)
        {
            throw new InvalidOperationException($"An active diagnostics ({_current.Name}) is available. Dispose it before starting a new one.");
        }

        onDispose ??=
            result =>
            {
                foreach (var message in result.Messages)
                {
                    try { Console.Build.MarkupLine(message.ToString()); }
                    catch { Console.WriteLine(message); }
                }

                if (result.Errors.Any())
                {
                    Environment.Exit(1);
                }
            };

        return _current = new Diagnostics(name, onDispose);
    }

    public static void Diagnose(Action action) =>
        Diagnose(() => { action(); return 0; });

    public static T? Diagnose<T>(Func<T> action)
    {
        try
        {
            return action();
        }
        catch (DiagnosticException ex)
        {
            Current._errors.Add(ex);
            ReportError(ex.Code, ex.Message);

            return default;
        }
        catch (Exception ex)
        {
            Current._errors.Add(ex);
            ReportError(DiagnosticCode.Unknown, ex.Message);
            if (ex.StackTrace is not null)
            {
                ReportInfo(Markup.Escape(ex.StackTrace));
            }

            return default;
        }
    }

    public static void ReportError(DiagnosticCode code, string message) =>
        Report(message, level: "error", code: code);

    public static void ReportWarning(DiagnosticCode code, string message) =>
        Report(message, level: "warning", code: code);

    public static void ReportInfo(string message) =>
        Report(message);

    static void Report(string message,
        string level = "info",
        DiagnosticCode? code = default
    ) => Current._messages.Add(new(message, level, code));

    Action<DiagnosticsResult> _dispose;
    bool _disposed;
    List<Exception> _errors = [];
    List<DiagnosticMessage> _messages = [];

    public string Name { get; }

    Diagnostics(string name, Action<DiagnosticsResult> onDispose)
    {
        Name = name;
        _dispose = onDispose;
    }

    public void OnDispose(Action<DiagnosticsResult> handler) =>
        _dispose = handler;

    void IDisposable.Dispose()
    {
        if (_disposed) { return; }

        _dispose(new(_errors.AsReadOnly(), _messages.AsReadOnly()));

        _current = null;
        _disposed = true;
    }

    ~Diagnostics()
    {
        if (_disposed) { return; }

        Console.WriteLine($"error: Diagnostics ({Name}) result was not disposed.");
        Environment.Exit(1);
    }
}