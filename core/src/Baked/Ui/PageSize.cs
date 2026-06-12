namespace Baked.Ui;

public record PageSize : Select, IComponentOverride<Select>
{
    public Select Base
    {
        set
        {
            Label = value.Label;
            LocalizeOptionLabels = value.LocalizeOptionLabels;
            OptionLabel = value.OptionLabel;
            OptionValue = value.OptionValue;
            ShowClear = value.ShowClear;
            Stateful = value.Stateful;
            Filter = value.Filter;
            TargetProp = value.TargetProp;
        }
    }
}