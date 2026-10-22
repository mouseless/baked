using Baked.Domain.Inspection;
using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public abstract class DomainModelContext
{
    Trace? _trace;

    public required DomainModel Domain { get; init; }
    public abstract string Identifier { get; }

    // NOTE contexts are shared among conventions, so trace is only available
    // while a convention is being applied, it throws otherwise instead of
    // returning the trace of another convention
    public Trace Trace
    {
        get => _trace ?? throw new InvalidOperationException(
            $"`{nameof(Trace)}` of `{Identifier}` is only available while a convention is being applied, " +
            $"assign it to a variable to use it in a delegate that runs later"
        );
        internal set => _trace = value;
    }

    internal void ClearTrace() =>
        _trace = null;
}