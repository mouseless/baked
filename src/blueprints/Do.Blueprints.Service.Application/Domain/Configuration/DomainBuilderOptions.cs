using System.Reflection;

namespace Do.Domain.Configuration;

public class DomainBuilderOptions
{
    public BindingFlags ConstuctorBindingFlags { get; set; } = BindingFlags.Default;
    public BindingFlags MethodBindingFlags { get; set; } = BindingFlags.Default;
    public BindingFlags PropertyBindingFlags { get; set; } = BindingFlags.Default;
}
