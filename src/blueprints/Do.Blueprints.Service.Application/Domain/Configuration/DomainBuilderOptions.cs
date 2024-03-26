using System.Reflection;

namespace Do.Domain.Configuration;

public class DomainBuilderOptions
{
    public BindingFlags ConstuctorBindingFlags { get; set; } = BindingFlags.Default;
    public BindingFlags MethodBindingFlags { get; set; } = BindingFlags.Default;
    public BindingFlags PropertyBindingFlags { get; set; } = BindingFlags.Default;

    public List<Func<ITypeDescriptor, bool>> AddProperties { get; } = [];
    public List<Func<ITypeDescriptor, bool>> AddMethods { get; } = [];
    public List<Func<ITypeDescriptor, bool>> AddInterfaces { get; } = [];
    public List<Func<ITypeDescriptor, bool>> AddBaseType { get; } = [];
    public List<Func<ITypeDescriptor, bool>> AddConstructor { get; } = [];
}

