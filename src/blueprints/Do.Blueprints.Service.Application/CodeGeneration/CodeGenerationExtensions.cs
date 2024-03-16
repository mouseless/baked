using Do.Architecture;
using Do.CodeGeneration;
using System.Reflection;

namespace Do;

public static class CodeGenerationExtensions
{
    public static void AddCodeGeneration(this ICollection<ILayer> layers) => layers.Add(new CodeGenerationLayer());

    public static ICodeCollection GetCodeCollection(this ApplicationContext source) => source.Get<ICodeCollection>();
    public static GeneratedAssemblies GetGeneratedAssemblies(this ApplicationContext source) => source.Get<GeneratedAssemblies>();
    public static Assembly GetGeneratedAssembly(this ApplicationContext source, string assemblyName) => source.GetGeneratedAssemblies()[assemblyName];

    public static void ConfigureCodeCollection(this LayerConfigurator configurator, Action<ICodeCollection> configuration) => configurator.Configure(configuration);
    public static void ConfigureCompilerOptions(this LayerConfigurator configurator, Action<CompilerOptions> configuration) => configurator.Configure(configuration);

    public static void AddCode(this ICodeCollection codes, string code, string assemblyName = "Default") => codes.Add(new(code, assemblyName));
}
