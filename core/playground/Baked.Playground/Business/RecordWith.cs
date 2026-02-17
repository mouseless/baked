namespace Baked.Playground.Business;

public record RecordWith<T>(
    T Single,
    IEnumerable<T> Enumerable,
    T[] Array
);