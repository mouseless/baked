namespace Baked.Playground.CodingStyle.RichTransient;

public record RecordWithRichTransientAsync(
    RichTransientAsync Single,
    IEnumerable<RichTransientAsync> Enumerable,
    RichTransientAsync[] Array
);