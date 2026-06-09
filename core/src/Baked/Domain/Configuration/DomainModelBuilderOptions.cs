using Baked.Buildtime.Diagnostics;
using Baked.Domain.Inspection;
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
    public ConventionOrderMatrixOptions ConventionOrderMatrix { get; } = new();
    public string? DefaultConventionLevel { get; set; }
    public Inspect Inspect { get; } = new();

    public class ConventionOrderMatrixOptions
    {
        public IList<string> Bases { get; } = [];
        public IList<string> Levels { get; } = [];
        public IList<string> Extensions { get; } = [];

        public Func<IDomainModelConvention, string>? FallbackBase { get; set; }
        public Func<IDomainModelConvention, string>? FallbackLevel { get; set; }
        public Func<IDomainModelConvention, string>? FallbackExtension { get; set; }
    }

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