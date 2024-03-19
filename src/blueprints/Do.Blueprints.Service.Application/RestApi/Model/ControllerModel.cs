namespace Do.RestApi.Model;

public record ControllerModel(string Name)
{
    public string Name { get; set; } = Name;
    public List<ActionModel> Actions { get; set; } = [];
}
