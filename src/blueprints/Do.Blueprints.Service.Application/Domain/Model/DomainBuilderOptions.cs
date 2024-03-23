using System.Reflection;
using Do.Domain.Configuration;

namespace Do.Domain.Model;

public class DomainBuilderOptions
{
    public BindingFlags ConstuctorBindingFlags { get; set; } = BindingFlags.Default;
    public BindingFlags MethodBindingFlags { get; set; } = BindingFlags.Default;
    public BindingFlags PropertyBindingFlags { get; set; } = BindingFlags.Default;

    public List<IIndexer> Indexers { get; } = [];
}
