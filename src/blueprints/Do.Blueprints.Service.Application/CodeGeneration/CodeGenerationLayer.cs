using Do.Architecture;
using Do.Domain.Model;

using static Do.CodeGeneration.CodeGenerationLayer;

namespace Do.CodeGeneration;

public class CodeGenerationLayer : LayerBase<GenerateCode>
{
    readonly ICodeCollection _codes = new CodeCollection();
    readonly CompilerOptions _compilerOptions = new();

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContextBuilder()
            .Add(_codes)
            .Add(_compilerOptions)
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new GenerateCode(_codes);
        yield return new Compile(_compilerOptions);
    }

    public class GenerateCode(ICodeCollection _codeCollection)
        : PhaseBase<DomainModel>(PhaseOrder.Earliest)
    {
        protected override void Initialize(DomainModel _)
        {
            Context.Add(_codeCollection);
        }
    }

    public class Compile(CompilerOptions _compilerOptions)
        : PhaseBase<ICodeCollection>(PhaseOrder.Latest)
    {
        protected override void Initialize(ICodeCollection codes)
        {
            var generatedAssemblies = new GeneratedAssemblies();

            var assemblyCodes = codes.GroupBy(cd => cd.AssemblyName, cd => cd.Code);
            foreach (var assemblyCode in assemblyCodes)
            {
                var compiler = new Compiler(_compilerOptions);
                foreach (var code in assemblyCode)
                {
                    compiler.AddCode(code);
                }

                var assembly = compiler.Compile();

                generatedAssemblies.Add(assemblyCode.Key, assembly);
            }

            Context.Add(generatedAssemblies);
        }
    }
}
