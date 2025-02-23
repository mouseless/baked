using Baked.Architecture;
using Baked.CodeGeneration;
using Newtonsoft.Json;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.Ui;

public class UiLayer : LayerBase<GenerateCode>
{
    public PageDescriptors _pageDescriptors = new();

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContextBuilder()
            .Add(_pageDescriptors)
            .OnDispose(GenerateUiSchemas)
            .Build();

    void GenerateUiSchemas()
    {
        var files = Context.Get<IGeneratedFileCollection>();
        foreach (var (key, page) in _pageDescriptors)
        {
            files.AddAsJson(key, page, outdir: "Ui", settings: new JsonSerializerSettings
            {
                ContractResolver = new AttributeAwareCamelCasePropertyNamesContractResolver()
            });
        }
    }
}