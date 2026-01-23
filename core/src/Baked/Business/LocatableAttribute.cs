using Baked.RestApi.Model;

namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableAttribute : Attribute
{
    public bool IsAsync { get; set; } = default!;
    public Func<ActionModelAttribute, ParameterModelAttribute> AddLocatorService { get; set; } = default!;
    public Func<ParameterModelAttribute, ParameterModelAttribute, string> FindTargetTemplate { get; set; } = default!;
    public Func<ParameterModelAttribute, string, bool, string> LookupParameterTemplate { get; set; } = default!;
    public Func<ParameterModelAttribute, string, bool, string> LookupListParameterTemplate { get; set; } = default!;
}