using Baked.Domain.Model;

namespace Baked.Domain.Metadata;

public class MetadataModelBuilderOptions
{
    public bool ExcludeTypesMissingAttributes { get; set; } = false;
    public Func<TypeModelMetadata, string> TypeGroupName { get; set; } = type => type.Name;

    public List<Type> TypeAttributes { get; set; } = [];
    public List<Type> MethodAttributes { get; set; } = [];
    public List<Type> ParameterAttributes { get; set; } = [];
    public List<Type> PropertyAttributes { get; set; } = [];
}