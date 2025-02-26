using Baked.Architecture;
using Baked.Domain.Model;
using System.Reflection;

using static Baked.CodeGeneration.CodeGenerationLayer;
using static Baked.Runtime.RuntimeLayer;

namespace Baked.CodeGeneration;

public class CodeGenerationLayer : LayerBase<GenerateCode, Compile, BuildConfiguration>
{
    readonly IGeneratedAssemblyCollection _generatedAssemblies = new GeneratedAssemblyCollection();
    readonly IGeneratedFileCollection _generatedFiles = new GeneratedFileCollection();
    readonly HashSet<string> _remainingFiles = [];

    string Location
    {
        get
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            environment = string.IsNullOrEmpty(environment) ? "Development" : environment;

            return Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? throw new("'EntryAssembly' should have existed with valid location"),
                environment
            );
        }
    }

    protected override PhaseContext GetContext(GenerateCode phase)
    {
        if (!Directory.Exists(Location))
        {
            Directory.CreateDirectory(Location);
        }

        _remainingFiles.UnionWith(Directory.GetFiles(Location, "*.*", SearchOption.AllDirectories))

        return phase.CreateContext(_generatedAssemblies);
    }

    protected override PhaseContext GetContext(Compile phase) =>
        phase.CreateContextBuilder()
            .Add(_generatedFiles)
            .OnDispose(() =>
            {
                foreach (var descriptor in _generatedFiles)
                {
                    var file = new GeneratedFileWriter(descriptor).Create(Location);

                    RemoveFromRemainingFiles(file);
                }

                DeleteRemainingFiles();
            })
            .Build();

    protected override PhaseContext GetContext(BuildConfiguration phase)
    {
        var files = Directory.GetFiles(Location);

        var generatedContext = new GeneratedContext();
        foreach (var file in files)
        {
            if (file.EndsWith(".hash")) { continue; }

            if (file.Contains("Baked.g"))
            {
                generatedContext.Assemblies.Add(Path.GetFileName(file).Replace("Baked.g.", string.Empty).Replace(".dll", string.Empty), Assembly.LoadFile(file));
            }
            else
            {
                generatedContext.Files.Add(Path.GetFileName(file)[..Path.GetFileName(file).LastIndexOf('.')], file);
            }
        }

        Context.Add(generatedContext);

        return PhaseContext.Empty;
    }

    protected override IEnumerable<IPhase> GetGeneratePhases()
    {
        yield return new GenerateCode(_generatedAssemblies, _generatedFiles);
        yield return new Compile(Location,
            onCompiled: RemoveFromRemainingFiles
        );
    }

    void RemoveFromRemainingFiles(string path)
    {
        _remainingFiles.Remove(path);
        _remainingFiles.Remove($"{path}.hash");
    }

    void DeleteRemainingFiles()
    {
        foreach (var file in _remainingFiles)
        {
            File.Delete(file);
            File.Delete($"{file}.hash");
        }
    }

    public class GenerateCode(IGeneratedAssemblyCollection _generatedAssemblies, IGeneratedFileCollection _generatedFiles)
        : PhaseBase<DomainModel>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _)
        {
            Context.Add(_generatedAssemblies);
            Context.Add(_generatedFiles);
        }
    }

    public class Compile(string _location,
        Action<string>? onCompiled = default
    ) : PhaseBase<IGeneratedAssemblyCollection>(PhaseOrder.Late)
    {
        protected override void Initialize(IGeneratedAssemblyCollection generatedAssemblies)
        {
            foreach (var descriptor in generatedAssemblies)
            {
                var assemblyPath = new Compiler(descriptor).Compile(_location, $"Baked.g.{descriptor.Name}");

                onCompiled?.Invoke(assemblyPath);
            }
        }
    }
}