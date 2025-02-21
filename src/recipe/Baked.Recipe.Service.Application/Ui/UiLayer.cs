using Baked.Architecture;
using Baked.CodeGeneration;
using Newtonsoft.Json;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.Ui;

public class UiLayer : LayerBase<GenerateCode>
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
            files.AddAsJson(key, component, outdir: "Ui", settings: new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
        }
    }
}