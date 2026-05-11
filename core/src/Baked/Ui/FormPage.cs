namespace Baked.Ui;

public record FormPage(string Path, PageTitle Title, Button Submit)
    : PageSchemaBase(Path)
{
    public PageTitle Title { get; set; } = Title;
    public Button Submit { get; set; } = Submit;
    public List<Section> Sections { get; init; } = [];

    public List<string>? ValidateComposable { get; set; } = ["useValidateDefault"];

    public record Section(string Key, string Label)
        : IOrderableSchema
    {
        public string Key { get; set; } = Key;
        public string Label { get; set; } = Label;
        public List<InputGroup> InputGroups { get; init; } = [];
    }

    public record InputGroup(string Key)
        : IOrderableSchema
    {
        public string Key { get; set; } = Key;
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