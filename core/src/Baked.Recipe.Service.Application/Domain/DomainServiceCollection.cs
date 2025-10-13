using System.Reflection;

namespace Baked.Domain;

public class DomainServiceCollection : List<DomainServiceDescriptor>
{
    public List<Assembly> References { get; init; } = [];
    public List<string> Usings { get; init; } = [];
}