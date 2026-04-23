namespace Baked;

public class Diagnostics : IDisposable
{
    static readonly string _cyan = "\x1b[1m\x1b[36m";
    static readonly string _reset = "\x1b[0m";

    static Diagnostics? _current;

    public static Diagnostics Current
    {
        get
        {
            if (_current is null)
            {
                throw new InvalidOperationException("There is no active diagnostics. Start one using `GenerationDiagnostics.Start(...)`");
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
                    Console.WriteLine(message);
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
        catch (DiagnosticsException ex)
        {
            Current._errors.Add(ex);
            ReportError(ex.Code, ex.Message);

            return default;
        }
        catch (Exception ex)
        {
            Current._errors.Add(ex);
            ReportError(DiagnosticsCode.Unknown, ex.Message);
            if (ex.StackTrace is not null)
            {
                ReportInfo(ex.StackTrace);
            }

            return default;
        }
    }

    public static void ReportError(DiagnosticsCode code, string message) =>
        Report(message, level: "error", code: code);

    public static void ReportWarning(DiagnosticsCode code, string message) =>
        Report(message, level: "warning", code: code);

    public static void ReportInfo(string message) =>
        Report(message);

    static void Report(string message,
        string level = "info",
        DiagnosticsCode? code = default
    )
    {
        if (level == "info") { level = $"{_cyan}info{_reset}"; }

        if (code is null)
        {
            Current._messages.Add($"{level}: {message}");
        }
        else if (code.Value.Key is null)
        {
            Current._messages.Add($"{level} C{code.Value.Number:D4}: {message}");
        }
        else
        {
            Current._messages.Add($"{level} B{code.Value.Number:D4}: {message} (See: https://baked.mouseless.codes/errors#{code.Value.Key})");
        }
    }

    Action<DiagnosticsResult> _dispose;
    bool _disposed;
    List<Exception> _errors = [];
    List<string> _messages = [];

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