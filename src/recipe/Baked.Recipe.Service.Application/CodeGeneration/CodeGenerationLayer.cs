using Baked.Architecture;
using Baked.Domain.Model;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.CodeGeneration;

public class CodeGenerationLayer : LayerBase<GenerateCode>
{
    readonly IGeneratedAssemblyCollection _generatedAssemblies = new GeneratedAssemblyCollection();

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContext(_generatedAssemblies);

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new LoadGeneratedAssemblies();
    }

    protected override IEnumerable<IPhase> GetGeneratePhases()
    {
        yield return new GenerateCode(_generatedAssemblies);
        yield return new Compile();
    }

    public class GenerateCode(IGeneratedAssemblyCollection _generatedAssemblies)
        : PhaseBase<DomainModel>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _)
        {
            Context.Add(_generatedAssemblies);
        }
    }

    public class Compile()
        : PhaseBase<IGeneratedAssemblyCollection>(PhaseOrder.Latest)
    {
        protected override void Initialize(IGeneratedAssemblyCollection generatedAssemblies)
        {
            var entryAssembly = Assembly.GetEntryAssembly() ?? throw new();
            var location = entryAssembly.Location.Replace($"{entryAssembly.GetName().Name}.dll", string.Empty);

            Dictionary<string, string> generatedAssemblyInfo = new Dictionary<string, string>();
            foreach (var descriptor in generatedAssemblies)
            {
                var assembly = new Compiler(descriptor).Compile(location, $"Baked.g.{descriptor.Name}");
                generatedAssemblyInfo[$"{descriptor.Name}"] = assembly.Location;
            }

            using (var file = new FileStream(Path.Combine(location, "GeneratedAssemblies.json"), FileMode.Create))
            {
                file.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(generatedAssemblyInfo)));
            }
        }
    }

    public class LoadGeneratedAssemblies() :
        PhaseBase(PhaseOrder.Early)
    {
        protected override void Initialize()
        {
            var entryAssembly = Assembly.GetEntryAssembly() ?? throw new();
            var location = entryAssembly.Location.Replace($"{entryAssembly.GetName().Name}.dll", string.Empty);

            var data = File.ReadAllText(Path.Combine(location, "GeneratedAssemblies.json"));
            var info = JsonConvert.DeserializeObject<Dictionary<string, string>>(data) ?? new Dictionary<string, string>();

            var provider = new GeneratedAssemblyProvider();
            foreach (var (key, value) in info)
            {
                provider.Add(key, Assembly.LoadFrom(value));
            }

            Context.Add(provider);
        }
    }
}