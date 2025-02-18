using Baked.Architecture;
using Baked.CodeGeneration;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.UI;

public class UILayer : LayerBase<GenerateCode>
{
    public ComponentDescriptors _componentDescriptors = new();

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContextBuilder()
        .Add(_componentDescriptors)
        .OnDispose(GenerateUISchemas)
        .Build();

    void GenerateUISchemas()
    {
        var files = Context.Get<IGeneratedFileCollection>();
        foreach (var (key, component) in _componentDescriptors)
        {
            files.AddAsJson(key, component);
        }
    }
}