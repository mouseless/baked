using Baked.Buildtime.Diagnostics;
using Spectre.Console;
using System.Diagnostics;

// NOTE namespace is at root for a better experience in Coding Style & UX
// development
namespace Baked;

public class Diagnostics : IDisposable
{
    const string ERROR = "error";
    const string WARNING = "warning";
    const string INFO = "info";

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

        return _current = new Diagnostics(name, onDispose ?? DefaultOnDispose);
    }

    static void DefaultOnDispose(DiagnosticsResult result)
    {
        foreach (var message in result.Messages)
        {
            try
            {
                Console.Build.MarkupLine(message.ToString());
            }
            catch { Console.WriteLine(message); }
        }

        var errorCount = result.Messages.Count(m => m.Level == ERROR);
        if (errorCount > 0)
        {
            Console.Build.WriteLine();
            Console.Build.MarkupLine($"Generate [bold maroon]failed with {errorCount} error(s)[/]");

            Environment.Exit(1);
        }
    }

    Action<DiagnosticsResult> _dispose;
    bool _disposed;
    List<Exception> _exceptions = [];
    List<DiagnosticMessage> _messages = [];

    public string Name { get; }

    Diagnostics(string name, Action<DiagnosticsResult> onDispose)
    {
        Name = name;
        _dispose = onDispose;
    }

    public void Diagnose(Action action) =>
        Diagnose(() => { action(); return 0; });

    public T? Diagnose<T>(Func<T> action)
    {
        try
        {
            return action();
        }
        catch (DiagnosticException ex)
        {
            _exceptions.Add(ex);
            var stackTrace = new StackTrace(ex, true);
            if (stackTrace.TryFindFeatureSource(out var source))
            {
                ReportError(ex.Code, $"{ex.Message} [gray]{source}[/]");
            }
            else
            {
                ReportError(ex.Code, ex.Message);
            }

            return default;
        }
        catch (Exception ex)
        {
            _exceptions.Add(ex);
            ReportError(DiagnosticCode.Unknown, ex.Message);
            if (ex.StackTrace is not null)
            {
                ReportInfo(Markup.Escape(ex.StackTrace));
            }

            return default;
        }
    }

    public void ReportError(DiagnosticCode code, string message) =>
        Report(message, level: ERROR, code: code);

    public void ReportWarning(DiagnosticCode code, string message) =>
        Report(message, level: WARNING, code: code);

    public void ReportInfo(string message,
        string? group = default
    ) => Report(message, group: group);

    void Report(string message,
        string level = INFO,
        DiagnosticCode? code = default,
        string? group = default
    ) => _messages.Add(new(message, level, code, group));

    public void OnDispose(Action<DiagnosticsResult> handler) =>
        _dispose = handler;

    void IDisposable.Dispose()
    {
        if (_disposed) { return; }

        var exceptions = _exceptions.AsReadOnly();
        var messages = _messages
            .GroupBy(m => m.Group)
            .OrderBy(g => g.Key)
            .SelectMany(g => g)
            .ToList()
            .AsReadOnly();

        _dispose(new(exceptions, messages));

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