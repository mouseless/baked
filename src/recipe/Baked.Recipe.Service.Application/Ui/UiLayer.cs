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
        if (!Directory.Exists(_componentDescriptors.SchemaDir))
        {
            throw new($"'{_componentDescriptors.SchemaDir}' directory does not exist");
        }

        var files = Context.Get<IGeneratedFileCollection>();
        foreach (var (key, component) in _componentDescriptors)
        {
            files.AddAsJson(key, component, outdir: _componentDescriptors.SchemaDir, settings: new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
        }
    }
}