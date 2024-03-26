using System.Reflection;

namespace Do.Domain.Configuration;

public class DomainBuilderOptions
{
    public ReferencedTypeOptions ReferencedType { get; } = new();
    public ReflectedTypeOptions ReflectedType { get; } = new();

    public class ReferencedTypeOptions
    {
        public List<Func<Type, bool>> ShouldSkipSetGenerics { get; set; } = [];
        public List<Func<Type, bool>> ShouldSkipSetInheritance { get; set; } = [];
    }

    public class ReflectedTypeOptions
    {
        public BindingFlags ConstructorBindingFlags { get; set; } = BindingFlags.Default;
        public BindingFlags MethodBindingFlags { get; set; } = BindingFlags.Default;
        public BindingFlags PropertyBindingFlags { get; set; } = BindingFlags.Default;
    }
}