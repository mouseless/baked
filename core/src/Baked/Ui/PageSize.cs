namespace Baked.Ui;

public record PageSize : Select, IComponentOverride<Select>
{
    public Select Base
    {
        set
        {
            Label = value.Label;
            OptionLabel = value.OptionLabel;
            OptionValue = value.OptionValue;
            LocalizeLabel = value.LocalizeLabel;
            ShowClear = value.ShowClear;
            Stateful = value.Stateful;
            Filter = value.Filter;
            TargetProp = value.TargetProp;
        }
    }
}