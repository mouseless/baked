using Do.Architecture;
using Do.CodeGeneration;
using System.Reflection;

namespace Do;

public static class CodeGenerationExtensions
{
    public static void AddCodeGeneration(this ICollection<ILayer> layers) => layers.Add(new CodeGenerationLayer());

    public static GeneratedAssemblies GetGeneratedAssemblies(this ApplicationContext source) => source.Get<GeneratedAssemblies>();
    public static Assembly GetGeneratedAssembly(this ApplicationContext source, string assemblyName) => source.GetGeneratedAssemblies()[assemblyName];

    public static void ConfigureCodeCollection(this LayerConfigurator configurator, Action<ICodeCollection> configuration) => configurator.Configure(configuration);
    public static void ConfigureCompilerOptions(this LayerConfigurator configurator, Action<CompilerOptions> configuration) => configurator.Configure(configuration);

    public static void AddCode(this ICodeCollection codeCollection, string code, string assemblyName = "Default") => codeCollection.Add(new(code, assemblyName));
}
