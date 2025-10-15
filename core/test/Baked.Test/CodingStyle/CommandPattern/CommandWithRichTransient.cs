using Baked.Test.CodingStyle.RichTransient;

namespace Baked.Test.CodingStyle.CommandPattern;

public class CommandWithRichTransient
{
    RichTransientWithData _transient = default!;

    public CommandWithRichTransient With(RichTransientWithData transient)
    {
        _transient = transient;

        return this;
    }

    public RichTransientWithData Execute() =>
        _transient;
}