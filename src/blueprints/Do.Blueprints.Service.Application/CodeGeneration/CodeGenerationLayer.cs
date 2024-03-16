using Do.Architecture;
using Do.Domain.Model;

using static Do.CodeGeneration.CodeGenerationLayer;

namespace Do.CodeGeneration;

public class CodeGenerationLayer : LayerBase<GenerateCode>
{
    readonly CompilerOptions _compiler = new();
    readonly ICodeCollection _codeCollection = new CodeCollection();

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContextBuilder()
            .Add<CompilerOptions>(_compiler)
            .Add<ICodeCollection>(_codeCollection)
            .Build();

    public class GenerateCode()
        : PhaseBase<DomainModel>(PhaseOrder.Earliest)
    {
        protected override void Initialize(DomainModel _)
        {
        }
    }
}
