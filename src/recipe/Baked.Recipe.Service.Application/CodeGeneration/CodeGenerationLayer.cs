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
    HashSet<string> _existingFiles = [];

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

        _existingFiles = [.. Directory.GetFiles(Location, "*.*", SearchOption.AllDirectories)];

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
                    _existingFiles.Remove(file);
                    _existingFiles.Remove(file + ".hash");
                }

                foreach (var file in _existingFiles)
                {
                    File.Delete(file);
                    File.Delete($"{file}.hash");
                }
            })
            .Build();

    protected override PhaseContext GetContext(BuildConfiguration phase)
    {
        var directoryfiles = Directory.GetFiles(Location);

        var generatedContext = new GeneratedContext();
        foreach (var path in directoryfiles)
        {
            if (path.Contains("Baked.g"))
            {
                if (!path.EndsWith(".dll")) { continue; }

                generatedContext.Assemblies.Add(Path.GetFileName(path).Replace("Baked.g.", string.Empty).Replace(".dll", string.Empty), Assembly.LoadFile(path));
            }
            else
            {
                if (path.EndsWith(".hash")) { continue; }

                generatedContext.Files.Add(Path.GetFileName(path)[..Path.GetFileName(path).LastIndexOf('.')], path);
            }
        }

        Context.Add(generatedContext);

        return PhaseContext.Empty;
    }

    protected override IEnumerable<IPhase> GetGeneratePhases()
    {
        yield return new GenerateCode(_generatedAssemblies, _generatedFiles);
        yield return new Compile(Location, _existingFiles);
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

    public class Compile(string _location, HashSet<string> _previousFiles)
        : PhaseBase<IGeneratedAssemblyCollection>(PhaseOrder.Late)
    {
        protected override void Initialize(IGeneratedAssemblyCollection generatedAssemblies)
        {
            foreach (var descriptor in generatedAssemblies)
            {
                var assembly = new Compiler(descriptor).Compile(_location, $"Baked.g.{descriptor.Name}");
                _previousFiles.Remove(assembly);
                _previousFiles.Remove($"{assembly}.hash");
            }
        }
    }
}