using Baked.Ui;

namespace Baked.Playground.Ui;

public record MyText : Text, IComponentOverride<Text>
{
    Text IComponentOverride<Text>.Base
    {
        set => MaxLength = value.MaxLength;
    }

    public string? SomethingExtra { get; set; }
}