using Do.Domain.Model;

namespace Do.RestApi.Model;

public record ControllerModel(TypeModel MappedType)
{
    public string Id { get; } = MappedType.CSharpFriendlyFullName;
    public string ClassName { get; set; } = MappedType.Name;
    public string GroupName { get; set; } = MappedType.Name;
    public Dictionary<string, ActionModel> Action { get; init; } = [];

    public IEnumerable<ActionModel> Actions { get => Action.Values.OrderBy(a => a.Order); init => Action = value.ToDictionary(a => a.Id); }
}