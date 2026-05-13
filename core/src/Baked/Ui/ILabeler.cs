namespace Baked.Ui;

public interface ILabeler : IComponentSchema
{
    string? Label { get; set; }
    string? LabelMode { get; set; }
    string? LabelVariant { get; set; }
    bool? ValidateLabel { get; set; }
}