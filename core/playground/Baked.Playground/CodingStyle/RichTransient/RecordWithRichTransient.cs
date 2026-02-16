namespace Baked.Playground.CodingStyle.RichTransient;

public record RecordWithRichTransient(
    RichTransientWithData Single,
    IEnumerable<RichTransientWithData> Enumerable,
    RichTransientWithData[] Array
);