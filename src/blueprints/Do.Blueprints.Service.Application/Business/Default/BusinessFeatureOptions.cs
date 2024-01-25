using System.Reflection;

namespace Do.Business.Default;

public record BusinessFeatureOptions(List<Assembly> BusinessAssemblies, List<Assembly> ApplicationParts);
