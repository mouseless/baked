namespace Baked.RestApi.Model;

[AttributeUsage(AttributeTargets.Class)]
public class ApiController()
    : Attribute()
{
    public ApiController(string id, string className, string groupName, IEnumerable<ApiAction> actions)
        : this()
    {
        Init(id, className, groupName, actions);

        Orphan = true;
    }

    public string Id { get; private set; } = default!;
    public string ClassName { get; set; } = default!;
    public string GroupName { get; set; } = default!;
    public Dictionary<string, ApiAction> Action { get; private set; } = default!;
    public bool Orphan { get; }
    internal bool Initialized { get; private set; } = false;

    public IEnumerable<ApiAction> Actions => Action.Values.OrderBy(a => a.Order);

    internal ApiController Init(string id, string className, string groupName, IEnumerable<ApiAction> actions)
    {
        if (Initialized) { throw new($"Cannot initialize, already initialized: {Id}"); }

        Id = id;
        ClassName ??= className;
        GroupName ??= groupName;
        Action = actions.ToDictionary(a => a.Id);
        Initialized = true;

        return this;
    }
}