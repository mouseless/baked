namespace Baked.RestApi.Model;

[AttributeUsage(AttributeTargets.Class)]
public class ControllerModel() : Attribute
{
    public ControllerModel(string id, string className, string groupName, IEnumerable<ActionModel> actions)
        : this()
    {
        Init(id, className, groupName, actions);

        ManuallyAdded = true;
    }

    public string Id { get; private set; } = default!;
    public string ClassName { get; set; } = default!;
    public string GroupName { get; set; } = default!;
    public Dictionary<string, ActionModel> Action { get; private set; } = default!;
    public bool ManuallyAdded { get; }

    public IEnumerable<ActionModel> Actions => Action.Values.OrderBy(a => a.Order);

    internal ControllerModel Init(string id, string defaultClassName, string defaultGroupName, IEnumerable<ActionModel> actions)
    {
        Id = id;
        ClassName ??= defaultClassName;
        GroupName ??= defaultGroupName;
        Action = actions.ToDictionary(a => a.Id);

        return this;
    }
}