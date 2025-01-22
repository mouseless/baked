using Baked.Architecture;
using Baked.Domain.Model;
using System.Reflection;
using System.Text;
using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.CodeGeneration;

public class CodeGenerationLayer : LayerBase<GenerateCode, Compile>
{
    readonly IGeneratedAssemblyCollection _generatedAssemblies = new GeneratedAssemblyCollection();
    readonly IGeneratedFileCollection _generatedFiles = new GeneratedFileCollection();
    readonly string _location = default!;

    public CodeGenerationLayer()
    {
        _location = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ??
            throw new("'EntryAssembly' should have existed with valid location");
        _location = Path.Combine(_location, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development");
    }

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContext(_generatedAssemblies);

    protected override PhaseContext GetContext(Compile phase) =>
        phase.CreateContext(_generatedFiles);

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new LoadGeneratedAssemblies(_location);
    }

    protected override IEnumerable<IPhase> GetBakePhases()
    {
        yield return new GenerateCode(_location, _generatedAssemblies, _generatedFiles);
        yield return new Compile(_location);
        yield return new CreateFiles(_location);
    }

    public class GenerateCode(string _location, IGeneratedAssemblyCollection _generatedAssemblies, IGeneratedFileCollection _generatedFiles)
        : PhaseBase<DomainModel>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _)
        {
            if (!Directory.Exists(_location))
            {
                Directory.CreateDirectory(_location);
            }
            else
            {
                Directory.Delete(_location, true);
                Directory.CreateDirectory(_location);
            }

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

    public class CreateFiles(string _location)
        : PhaseBase<IGeneratedFileCollection>(PhaseOrder.Latest)
    {
        protected override void Initialize(IGeneratedFileCollection generatedFiles)
        {
            foreach (var descriptor in generatedFiles)
            {
                using (var file = new FileStream(Path.Combine(_location, $"{descriptor.Name}.{descriptor.Extension}"), FileMode.Create))
                {
                    file.Write(Encoding.UTF8.GetBytes(descriptor.Content));
                }
            }
        }
    }

    public class LoadGeneratedAssemblies(string _location)
        : PhaseBase(PhaseOrder.Early)
    {
        protected override void Initialize()
        {
            var directoryfiles = Directory.GetFiles(_location);

            var provider = new GeneratedAssemblyProvider();
            foreach (var path in directoryfiles.Where(s => s.Contains("Baked.g")))
            {
                provider.Add(Path.GetFileName(path).Replace("Baked.g.", string.Empty).Replace(".dll", string.Empty), Assembly.LoadFile(path));
            }

            Context.Add(provider);

            var fileProvider = new GeneratedFileProvider();
            foreach (var path in directoryfiles.Where(s => !s.Contains("Baked.g")))
            {
                fileProvider.Add(Path.GetFileName(path)[..Path.GetFileName(path).LastIndexOf('.')], path);
            }

            Context.Add(fileProvider);
        }
    }
}