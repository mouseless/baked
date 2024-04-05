using System.Reflection;

namespace Do.RestApi.Model;

public record ApiModel
{
    public List<Assembly> References { get; init; } = [];
    public Dictionary<string, ControllerModel> Controller { get; init; } = [];

    public IEnumerable<ControllerModel> Controllers { get => Controller.Values; init => Controller = value.ToDictionary(c => c.Id); }
}
