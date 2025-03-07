﻿namespace Baked.RestApi.Model;

[AttributeUsage(AttributeTargets.Class)]
public class ControllerModelAttribute() : Attribute
{
    public ControllerModelAttribute(string id, string className, string groupName, IEnumerable<ActionModelAttribute> actions)
        : this()
    {
        Init(id, className, groupName, actions);

        Orphan = true;
    }

    public string Id { get; private set; } = default!;
    public string ClassName { get; set; } = default!;
    public string GroupName { get; set; } = default!;
    public Dictionary<string, ActionModelAttribute> Action { get; private set; } = default!;
    public bool Orphan { get; }
    internal bool Initialized { get; private set; } = false;

    public IEnumerable<ActionModelAttribute> Actions => Action.Values.OrderBy(a => a.Order);

    internal ControllerModelAttribute Init(string id, string className, string groupName, IEnumerable<ActionModelAttribute> actions)
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