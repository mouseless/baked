using System.Reflection;

namespace Do.RestApi.Model;

public record ApiModel
{
    public List<Assembly> References { get; set; } = [];
    public List<ControllerModel> Controllers { get; set; } = [];
}
