using Baked.Ui;

namespace Baked.Theme.Admin;

public class GenericPage(string Path, PageTitle Title) : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
    public PageTitle Title { get; set; } = Title;
    public List<IComponentDescriptor> Components { get; init; } = [];
}