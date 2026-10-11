namespace Baked.Ui;

public record FormPage : PageSchemaBase
{
    public IComponentDescriptor Title { get; set; } = MissingComponent.Empty;
    public Button Submit { get; set; } = new();
    public List<Section> Sections { get; init; } = [];
    public List<ValidationComposable>? Validations { get; set; }
    public bool? ShowValidationSummary { get; set; }

    public record Section : IOrderableSchema
    {
        public string Key { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public List<InputGroup> InputGroups { get; init; } = [];
    }

    public record InputGroup : IOrderableSchema
    {
        public string Key { get; set; } = string.Empty;
        public List<Input> Inputs { get; init; } = [];
        public bool? Wide { get; set; }
    }

    public void ForEachInputGroup(Action<InputGroup> action)
    {
        foreach (var section in Sections)
        {
            foreach (var group in section.InputGroups)
            {
                action(group);
            }
        }
    }

    public void ForEachInput(Action<Input> action)
    {
        foreach (var section in Sections)
        {
            foreach (var group in section.InputGroups)
            {
                foreach (var input in group.Inputs)
                {
                    action(input);
                }
            }
        }
    }
}