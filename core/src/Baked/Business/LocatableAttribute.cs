using Baked.RestApi.Model;

namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableAttribute : Attribute
{
    public Func<ActionModelAttribute, ParameterModelAttribute> AddLocatorService { get; set; } = default!;
    public Func<ParameterModelAttribute, ParameterModelAttribute, string> LocateTargetTemplate { get; set; } = default!;
    public Func<ParameterModelAttribute, string, string> LookupParameterTemplate { get; set; } = default!;
}