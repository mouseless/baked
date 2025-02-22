using System.Reflection;

namespace Baked.RestApi.Model;

public record ApiModel
{
    public List<Assembly> References { get; init; } = [];
    public List<string> Usings { get; } = [];
    public Dictionary<string, ControllerModel> Controller { get; init; } = [];

    public IEnumerable<ControllerModel> Controllers => Controller.Values;
}