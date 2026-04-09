namespace Baked.Domain.Metadata;

public class MetadataModelBuilderOptions
{
    public List<Type> TypeAttributes { get; set; } = [];
    public bool ExcludeTypesMissingAttributes { get; set; } = true;
    public List<Type> MethodAttributes { get; set; } = [];
    public List<Type> ParameterAttributes { get; set; } = [];
    public List<Type> PropertyAttributes { get; set; } = [];
}