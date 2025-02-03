using System.Reflection;

namespace Baked.Domain;

public class DomainServicesModel
{
    public List<Assembly> References { get; init; } = [];
    public List<string> Usings { get; init; } = [];
    public List<DomainServiceDescriptor> Services { get; init; } = [];
}