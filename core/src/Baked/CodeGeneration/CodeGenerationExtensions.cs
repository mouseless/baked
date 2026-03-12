using Baked.Architecture;
using Baked.CodeGeneration;
using Baked.Testing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Reflection;

namespace Baked;

public static class CodeGenerationExtensions
{
    extension(ICollection<ILayer> layers)
    {
        public void AddCodeGeneration() =>
            layers.Add(new CodeGenerationLayer());
    }

    extension(ApplicationContext context)
    {
        public IGeneratedAssemblyCollection GetGeneratedAssemblyCollection() =>
            context.Get<IGeneratedAssemblyCollection>();

        public GeneratedContext GetGeneratedContext() =>
            context.Get<GeneratedContext>();

        public Assembly GetGeneratedAssembly(string name) =>
            context.Get<GeneratedContext>().Assemblies[name];
    }

    extension(LayerConfigurator configurator)
    {
        public void ConfigureGeneratedAssemblyCollection(Action<IGeneratedAssemblyCollection> configuration) =>
            configurator.Configure(configuration);

        public void ConfigureGeneratedFileCollection(Action<IGeneratedFileCollection> configuration) =>
           configurator.Configure(configuration);

        public void UsingGeneratedContext(Action<GeneratedContext> configuration) =>
            configurator.Use(configuration);
    }

    extension(IGeneratedAssemblyCollection generatedAssemblies)
    {
        /// <summary>
        /// Adds a descriptor for a generated assembly with given parameters
        ///
        /// ℹ️  Any layer or feature generates code which accesses non-public members should
        /// be explicitly added tobe added to abstraction project `.targets` file
        /// </summary>
        public void Add(string name, Action<GeneratedAssemblyDescriptor> descriptorBuilder,
            Func<CSharpCompilationOptions, CSharpCompilationOptions>? compilationOptionsBuilder = default
        )
        {
            var descriptor = new GeneratedAssemblyDescriptor(name);

            descriptorBuilder(descriptor);
            descriptor.CompilationOptions = compilationOptionsBuilder?.Invoke(descriptor.CompilationOptions) ?? descriptor.CompilationOptions;

            generatedAssemblies.Add(descriptor);
        }

        public void Add(string name, Action<GeneratedAssemblyDescriptor> descriptorBuilder,
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
    }

    extension(GeneratedAssemblyDescriptor descriptor)
    {
        public GeneratedAssemblyDescriptor AddCode(string code) =>
            descriptor.AddCodes([code]);

        public GeneratedAssemblyDescriptor AddCodes(ICodeTemplate codeTemplate) =>
            descriptor
              .AddReferences(codeTemplate.References)
              .AddCodes([.. codeTemplate.Render()]);

        public GeneratedAssemblyDescriptor AddCodes(params IEnumerable<string> codes)
        {
            descriptor.Codes.AddRange(codes);

            return descriptor;
        }

        public GeneratedAssemblyDescriptor AddReferenceFrom<T>() => descriptor.AddReferenceFrom(typeof(T));
        public GeneratedAssemblyDescriptor AddReferenceFrom(Type type) => descriptor.AddReference(type.Assembly);
        public GeneratedAssemblyDescriptor AddReference(Assembly reference) => descriptor.AddReferences([reference]);
        public GeneratedAssemblyDescriptor AddReferences(params IEnumerable<Assembly> references)
        {
            descriptor.References.AddRange(references);

            return descriptor;
        }
    }

    extension(IGeneratedFileCollection generatedFiles)
    {
        public void Add(string name, string content,
            string? extension = default,
            string? outdir = default
        )
        {
            extension ??= "txt";
            extension = extension[(extension.LastIndexOf('.') + 1)..];

            generatedFiles.Add(new(name) { Content = content, Extension = extension, Outdir = outdir });
        }

        public void AddAsJson<T>(T instance,
            string? name = default,
            string? outdir = default
        ) => generatedFiles.AddAsJson(name ?? typeof(T).Name, instance, outdir: outdir);

        public void AddAsJson<T>(string name, T instance,
            JsonSerializerSettings? settings = default,
            string? outdir = default
        )
        {
            settings ??= new();
            settings.Formatting = Formatting.Indented;

            var writer = new StringWriter();
            JsonSerializer.Create(settings).Serialize(writer, instance);
            generatedFiles.Add(name, writer.ToString(), "json", outdir: outdir);
        }
    }

    extension(Diagnostic diagnostic)
    {
        internal string? FindClosestScopedCode()
        {
            var tree = diagnostic.Location.SourceTree;
            if (tree is null) { return null; }

            return tree.GetRoot().FindNode(diagnostic.Location.SourceSpan).GetScopeNode()?.ToString() ?? diagnostic.Location.SourceTree?.ToString();
        }
    }

    static SyntaxNode? GetScopeNode(this SyntaxNode? node)
    {
        if (node is null) { return null; }
        if (node is MethodDeclarationSyntax or ClassDeclarationSyntax) { return node; }

        return GetScopeNode(node?.Parent);
    }

    extension(Stubber _)
    {
        public string ALocation() =>
            Path.GetDirectoryName(Assembly.GetCallingAssembly().Location ?? string.Empty) ?? string.Empty;
    }
}