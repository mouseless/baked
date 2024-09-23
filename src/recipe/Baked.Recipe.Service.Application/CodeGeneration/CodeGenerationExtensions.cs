using Baked.Architecture;
using Baked.CodeGeneration;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace Baked;

public static class CodeGenerationExtensions
{
    public static void AddCodeGeneration(this ICollection<ILayer> layers) =>
        layers.Add(new CodeGenerationLayer());

    public static IGeneratedAssemblyCollection GetGeneratedAssemblyCollection(this ApplicationContext context) =>
        context.Get<IGeneratedAssemblyCollection>();

    public static GeneratedAssemblyProvider GetGeneratedAssemblyProvider(this ApplicationContext context) =>
        context.Get<GeneratedAssemblyProvider>();

    public static Assembly GetGeneratedAssembly(this ApplicationContext context, string name) =>
        context.GetGeneratedAssemblyProvider()[name];

    public static void ConfigureGeneratedAssemblyCollection(this LayerConfigurator configurator, Action<IGeneratedAssemblyCollection> configuration) =>
        configurator.Configure(configuration);

    /// <summary>
    /// Adds a descriptor for a generated assembly with given parameters
    /// 
    /// ℹ️  Any layer or feature generates code which accesses non-public members should
    /// be explicitly added tobe added to abstraction project `.targets` file  
    /// </summary>
    public static void Add(this IGeneratedAssemblyCollection generatedAssemblies, string name, Action<GeneratedAssemblyDescriptor> descriptorBuilder,
        Func<CSharpCompilationOptions, CSharpCompilationOptions>? compilationOptionsBuilder = default
    )
    {
        var descriptor = new GeneratedAssemblyDescriptor(name);

        descriptorBuilder(descriptor);
        descriptor.CompilationOptions = compilationOptionsBuilder?.Invoke(descriptor.CompilationOptions) ?? descriptor.CompilationOptions;

        generatedAssemblies.Add(descriptor);
    }

    public static GeneratedAssemblyDescriptor AddCode(this GeneratedAssemblyDescriptor descriptor, string code) => descriptor.AddCodes(code);
    public static GeneratedAssemblyDescriptor AddCodes(this GeneratedAssemblyDescriptor descriptor, ICodeTemplate codeTemplate) => descriptor.AddCodes(codeTemplate.Render());
    public static GeneratedAssemblyDescriptor AddCodes(this GeneratedAssemblyDescriptor descriptor, IEnumerable<string> codes) => descriptor.AddCodes(codes.ToArray());
    public static GeneratedAssemblyDescriptor AddCodes(this GeneratedAssemblyDescriptor descriptor, params string[] codes)
    {
        descriptor.Codes.AddRange(codes);

        return descriptor;
    }

    public static GeneratedAssemblyDescriptor AddReferenceFrom<T>(this GeneratedAssemblyDescriptor descriptor) => descriptor.AddReferenceFrom(typeof(T));
    public static GeneratedAssemblyDescriptor AddReferenceFrom(this GeneratedAssemblyDescriptor descriptor, Type type) => descriptor.AddReference(type.Assembly);
    public static GeneratedAssemblyDescriptor AddReference(this GeneratedAssemblyDescriptor descriptor, Assembly reference) => descriptor.AddReferences(reference);
    public static GeneratedAssemblyDescriptor AddReferences(this GeneratedAssemblyDescriptor descriptor, IEnumerable<Assembly> references) => descriptor.AddReferences(references.ToArray());
    public static GeneratedAssemblyDescriptor AddReferences(this GeneratedAssemblyDescriptor descriptor, params Assembly[] references)
    {
        descriptor.References.AddRange(references);

        return descriptor;
    }
}