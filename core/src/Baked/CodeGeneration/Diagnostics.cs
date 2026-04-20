using System.Diagnostics.CodeAnalysis;

namespace Baked.CodeGeneration;

public class Diagnostics : IDisposable
{
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
            Report(ex.Message, level: "error");

            return default;
        }
    }

    [DoesNotReturn]
    public static void ReportError(string message) =>
        throw new DiagnosticsException(message);

    public static void ReportWarning(string message) =>
        Report(message, level: "warning");

    public static void ReportInfo(string message) =>
        Report(message, level: "info");

    public static void Report(string message,
        string level = "info"
    ) => Current._messages.Add($"{level}: {message}");

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