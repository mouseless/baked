using Do.Architecture;
using Do.CodeGeneration;

namespace Do;

public static class CodeGenerationExtensions
{
    public static void AddCodeGeneration(this ICollection<ILayer> layers) => layers.Add(new CodeGenerationLayer());

    public static GeneratedAssembly GetGeneratedAssembly(this ApplicationContext source) => source.Get<GeneratedAssembly>();

    public static void ConfigureCodeCollection(this LayerConfigurator configurator, Action<ICodeCollection> configuration) => configurator.Configure(configuration);
    public static void ConfigureCompilerOptions(this LayerConfigurator configurator, Action<CompilerOptions> configuration) => configurator.Configure(configuration);
}
