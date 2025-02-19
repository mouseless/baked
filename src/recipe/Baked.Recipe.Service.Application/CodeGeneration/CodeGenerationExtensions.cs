using Baked.Architecture;
using Baked.CodeGeneration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Reflection;

namespace Baked;

public static class CodeGenerationExtensions
{
    public static void AddCodeGeneration(this ICollection<ILayer> layers) =>
        layers.Add(new CodeGenerationLayer());

    public static IGeneratedAssemblyCollection GetGeneratedAssemblyCollection(this ApplicationContext context) =>
        context.Get<IGeneratedAssemblyCollection>();

    public static GeneratedContext GetGeneratedContext(this ApplicationContext context) =>
        context.Get<GeneratedContext>();

    public static Assembly GetGeneratedAssembly(this ApplicationContext context, string name) =>
        context.Get<GeneratedContext>().Assemblies[name];

    public static void ConfigureGeneratedAssemblyCollection(this LayerConfigurator configurator, Action<IGeneratedAssemblyCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureGeneratedFileCollection(this LayerConfigurator configurator, Action<IGeneratedFileCollection> configuration) =>
       configurator.Configure(configuration);

    public static void UsingGeneratedContext(this LayerConfigurator configurator, Action<GeneratedContext> configuration) =>
        configurator.Use(configuration);

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

    public static void Add(this IGeneratedAssemblyCollection generatedAssemblies, string name, Action<GeneratedAssemblyDescriptor> descriptorBuilder,
        List<string>? usings = default
    )
    {
        usings ??= [];
        usings.AddRange([
            "Baked",
            "System",
            "System.Linq",
            "System.Collections",
            "System.Collections.Generic",
            "System.Threading.Tasks"
        ]);

        var descriptor = new GeneratedAssemblyDescriptor(name);

        descriptorBuilder(descriptor);
        descriptor.CompilationOptions = descriptor.CompilationOptions.WithUsings(usings);

        generatedAssemblies.Add(descriptor);
    }

    public static GeneratedAssemblyDescriptor AddCode(this GeneratedAssemblyDescriptor descriptor, string code) => descriptor.AddCodes(code);
    public static GeneratedAssemblyDescriptor AddCodes(this GeneratedAssemblyDescriptor descriptor, ICodeTemplate codeTemplate) => descriptor.AddCodes([.. codeTemplate.Render()]);
    public static GeneratedAssemblyDescriptor AddCodes(this GeneratedAssemblyDescriptor descriptor, params IEnumerable<string> codes)
    {
        descriptor.Codes.AddRange(codes);

        return descriptor;
    }

    public static GeneratedAssemblyDescriptor AddReferenceFrom<T>(this GeneratedAssemblyDescriptor descriptor) => descriptor.AddReferenceFrom(typeof(T));
    public static GeneratedAssemblyDescriptor AddReferenceFrom(this GeneratedAssemblyDescriptor descriptor, Type type) => descriptor.AddReference(type.Assembly);
    public static GeneratedAssemblyDescriptor AddReference(this GeneratedAssemblyDescriptor descriptor, Assembly reference) => descriptor.AddReferences(reference);
    public static GeneratedAssemblyDescriptor AddReferences(this GeneratedAssemblyDescriptor descriptor, params IEnumerable<Assembly> references)
    {
        descriptor.References.AddRange(references);

        return descriptor;
    }

    public static void Add(this IGeneratedFileCollection generatedFiles, string name, string content,
        string? extension = default,
        string? outdir = default
    )
    {
        extension ??= "txt";
        extension = extension[(extension.LastIndexOf('.') + 1)..];

        generatedFiles.Add(new(name) { Content = content, Extension = extension, Outdir = outdir });
    }

    public static void AddAsJson<T>(this IGeneratedFileCollection generatedFiles, T instance) =>
        generatedFiles.AddAsJson(typeof(T).Name, instance);

    public static void AddAsJson<T>(this IGeneratedFileCollection generatedFiles, string name, T instance,
        JsonSerializerSettings? settings = default,
        string? outdir = default
    ) => generatedFiles.Add(name, JsonConvert.SerializeObject(instance, formatting: Formatting.Indented, settings), "json",
            outdir: outdir
        );

    internal static string? FindClosestScopedCode(this Diagnostic diagnostic)
    {
        var tree = diagnostic.Location.SourceTree;
        if (tree is null) { return null; }

        return GetScopeNode(tree.GetRoot().FindNode(diagnostic.Location.SourceSpan))?.ToString() ?? diagnostic.Location.SourceTree?.ToString();
    }

    static SyntaxNode? GetScopeNode(SyntaxNode? node)
    {
        if (node is null) { return null; }
        if (node is MethodDeclarationSyntax or ClassDeclarationSyntax) { return node; }

        return GetScopeNode(node?.Parent);
    }
}