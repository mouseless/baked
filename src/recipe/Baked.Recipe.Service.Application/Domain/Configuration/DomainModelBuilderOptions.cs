using Baked.Domain.Model;
using System.Reflection;

namespace Baked.Domain.Configuration;

public class DomainModelBuilderOptions
{
    public ICollection<TypeBuildLevelFilter> BuildLevels { get; set; } = [];
    public BindingFlagOptions BindingFlags { get; } = new();
    public IDomainModelConventionCollection Conventions { get; set; } = new DomainModelConventionCollection();
    public DomainIndexOptions Index { get; set; } = new();
    public Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel> DefaultOverloadSelector { get; set; } = overloads => overloads.First();

    public class BindingFlagOptions
    {
        public BindingFlags Constructor { get; set; } = System.Reflection.BindingFlags.Default;
        public BindingFlags Method { get; set; } = System.Reflection.BindingFlags.Default;
        public BindingFlags Property { get; set; } = System.Reflection.BindingFlags.Default;
    }

    public class DomainIndexOptions
    {
        public ICollection<Type> Type { get; } = [];
        public ICollection<Type> Method { get; } = [];
        public ICollection<Type> Parameter { get; } = [];
        public ICollection<Type> Property { get; } = [];
    }
}