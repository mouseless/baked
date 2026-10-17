using System.ComponentModel.DataAnnotations;

namespace Baked.Playground.Business;

public record AnnotatedRecord(
    [property: StringLength(5)] string Text,
    [property: Range(1, 10)] int Numeric
);