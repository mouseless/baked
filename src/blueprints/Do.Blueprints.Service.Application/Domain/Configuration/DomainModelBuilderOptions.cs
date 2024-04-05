using Do.Domain.Model;
using System.Reflection;

namespace Do.Domain.Configuration;

public class DomainModelBuilderOptions
{
    public ICollection<TypeBuildLevelFilter> BuildLevels { get; set; } = [];
    public BindingFlagOptions BindingFlags { get; } = new();
    public DomainMetadataOptions Metadata { get; set; } = new();
    public DomainIndexOptions Index { get; set; } = new();

    public class BindingFlagOptions
    {
        public BindingFlags Constructor { get; set; } = System.Reflection.BindingFlags.Default;
        public BindingFlags Method { get; set; } = System.Reflection.BindingFlags.Default;
        public BindingFlags Property { get; set; } = System.Reflection.BindingFlags.Default;
    }

    public class DomainMetadataOptions
    {
        public ICollection<MetadataConvention<TypeModel>> Type { get; } = [];
        public ICollection<MetadataConvention<MethodModel>> Method { get; } = [];
        public ICollection<MetadataConvention<ParameterModel>> Parameter { get; } = [];
        public ICollection<MetadataConvention<PropertyModel>> Property { get; } = [];
    }

    public class DomainIndexOptions
    {
        public ICollection<Type> Type { get; } = [];
        public ICollection<Type> Method { get; } = [];
        public ICollection<Type> Parameter { get; } = [];
        public ICollection<Type> Property { get; } = [];
    }
}