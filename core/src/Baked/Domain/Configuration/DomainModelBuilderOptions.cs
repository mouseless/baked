using Baked.Buildtime.Diagnostics;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked.Domain.Configuration;

public class DomainModelBuilderOptions
{
    public ICollection<TypeBuildLevelFilter> BuildLevels { get; set; } = [];
    public BindingFlagOptions BindingFlags { get; } = new();
    public DomainIndexOptions Index { get; set; } = new();
    public Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel> DefaultOverloadSelector { get; set; } = overloads => overloads.First();
    public Action<DiagnosticsResult>? OnComplete { get; set; }
    public IList<string> ConventionLevels { get; } = [];
    public string DefaultConventionLevel { get; set; } = nameof(Domain);

    public class BindingFlagOptions
    {
        public BindingFlags Constructor { get; set; } = System.Reflection.BindingFlags.Default;
        public BindingFlags Method { get; set; } = System.Reflection.BindingFlags.Default;
        public BindingFlags Property { get; set; } = System.Reflection.BindingFlags.Default;
    }

    public class DomainIndexOptions
    {
        public ICollection<Type> Type { get; } = [];
        public ICollection<Type> Property { get; } = [];
        public ICollection<Type> Method { get; } = [];
        public ICollection<Type> Parameter { get; } = [];
    }
}