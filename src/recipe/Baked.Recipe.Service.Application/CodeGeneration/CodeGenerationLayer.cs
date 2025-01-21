using Baked.Architecture;
using Baked.Domain.Model;
using System.Reflection;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.CodeGeneration;

public class CodeGenerationLayer : LayerBase<GenerateCode>
{
    readonly IGeneratedAssemblyCollection _generatedAssemblies = new GeneratedAssemblyCollection();
    readonly string _location = default!;

    public CodeGenerationLayer()
    {
        _location = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ??
            throw new("'EntryAssembly' should have existed with valid location");
        _location = Path.Combine(_location, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development");
    }

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContext(_generatedAssemblies);

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new LoadGeneratedAssemblies(_location);
    }

    protected override IEnumerable<IPhase> GetBakePhases()
    {
        yield return new GenerateCode(_generatedAssemblies);
        yield return new Compile(_location);
    }

    public class GenerateCode(IGeneratedAssemblyCollection _generatedAssemblies)
        : PhaseBase<DomainModel>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _)
        {
            Context.Add(_generatedAssemblies);
        }
    }

    public class Compile(string location)
        : PhaseBase<IGeneratedAssemblyCollection>(PhaseOrder.Latest)
    {
        protected override void Initialize(IGeneratedAssemblyCollection generatedAssemblies)
        {
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }

            foreach (var descriptor in generatedAssemblies)
            {
                new Compiler(descriptor).Compile(location, $"Baked.g.{descriptor.Name}");
            }
        }
    }

    public class LoadGeneratedAssemblies(string location)
        : PhaseBase(PhaseOrder.Early)
    {
        protected override void Initialize()
        {
            var info = Directory.GetFiles(location).Where(s => s.Contains("Baked.g"))
                .ToDictionary(s => Path.GetFileName(s).Replace("Baked.g.", string.Empty).Replace(".dll", string.Empty), s => s);

            var provider = new GeneratedAssemblyProvider();
            foreach (var (key, value) in info)
            {
                provider.Add(key, Assembly.LoadFile(value));
            }

            Context.Add(provider);
        }
    }
}