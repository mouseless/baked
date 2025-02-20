using Baked.Architecture;
using Baked.Domain.Model;
using System.Reflection;
using System.Text;

using static Baked.CodeGeneration.CodeGenerationLayer;
using static Baked.Runtime.RuntimeLayer;

namespace Baked.CodeGeneration;

public class CodeGenerationLayer : LayerBase<GenerateCode, Compile, BuildConfiguration>
{
    readonly IGeneratedAssemblyCollection _generatedAssemblies = new GeneratedAssemblyCollection();
    readonly IGeneratedFileCollection _generatedFiles = new GeneratedFileCollection();
    readonly string _location = default!;

    public CodeGenerationLayer()
    {
        _location = Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? throw new("'EntryAssembly' should have existed with valid location"),
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"
        );
    }

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContext(_generatedAssemblies);

    protected override PhaseContext GetContext(Compile phase) =>
        phase.CreateContextBuilder()
            .Add(_generatedFiles)
            .OnDispose(() =>
            {
                foreach (var descriptor in _generatedFiles)
                {
                    var directory = _location;
                    if (descriptor.Outdir != null)
                    {
                        if (Path.IsPathRooted(descriptor.Outdir))
                        {
                            directory = descriptor.Outdir;
                        }
                        else
                        {
                            directory = Path.Combine(_location, descriptor.Outdir);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
                        }
                    }

                    using var file = new FileStream(Path.Combine(directory, $"{descriptor.Name}.{descriptor.Extension}"), FileMode.Create);
                    file.Write(Encoding.UTF8.GetBytes(descriptor.Content));
                }
            })
            .Build();

    protected override PhaseContext GetContext(BuildConfiguration phase)
    {
        var directoryfiles = Directory.GetFiles(_location);

        var generatedContext = new GeneratedContext();
        foreach (var path in directoryfiles)
        {
            if (path.Contains("Baked.g"))
            {
                generatedContext.Assemblies.Add(Path.GetFileName(path).Replace("Baked.g.", string.Empty).Replace(".dll", string.Empty), Assembly.LoadFile(path));
            }
            else
            {
                generatedContext.Files.Add(Path.GetFileName(path)[..Path.GetFileName(path).LastIndexOf('.')], path);
            }
        }

        Context.Add(generatedContext);

        return PhaseContext.Empty;
    }

    protected override IEnumerable<IPhase> GetGeneratePhases()
    {
        yield return new GenerateCode(_location, _generatedAssemblies, _generatedFiles);
        yield return new Compile(_location);
    }

    public class GenerateCode(string _location, IGeneratedAssemblyCollection _generatedAssemblies, IGeneratedFileCollection _generatedFiles)
        : PhaseBase<DomainModel>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _)
        {
            if (Directory.Exists(_location))
            {
                Directory.Delete(_location, true);
            }

            Directory.CreateDirectory(_location);

            Context.Add(_generatedAssemblies);
            Context.Add(_generatedFiles);
        }
    }

    public class Compile(string _location)
        : PhaseBase<IGeneratedAssemblyCollection>(PhaseOrder.Late)
    {
        protected override void Initialize(IGeneratedAssemblyCollection generatedAssemblies)
        {
            foreach (var descriptor in generatedAssemblies)
            {
                new Compiler(descriptor).Compile(_location, $"Baked.g.{descriptor.Name}");
            }
        }
    }
}