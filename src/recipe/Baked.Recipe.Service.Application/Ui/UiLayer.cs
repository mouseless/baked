using Baked.Architecture;
using Baked.CodeGeneration;
using Newtonsoft.Json;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.Ui;

public class UiLayer : LayerBase<GenerateCode>
{
    public LayoutDescriptors _layoutDescriptors = new();
    public PageDescriptors _pageDescriptors = new();

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContextBuilder()
            .Add(_layoutDescriptors)
            .Add(_pageDescriptors)
            .OnDispose(GenerateUiSchemas)
            .Build();

    void GenerateUiSchemas()
    {
        var files = Context.Get<IGeneratedFileCollection>();
        foreach (var (key, layout) in _layoutDescriptors)
        {
            files.AddAsJson($"{key}.layout", layout, outdir: "Ui", settings: JsonSettings);
        }

        foreach (var (key, page) in _pageDescriptors)
        {
            files.AddAsJson(key, page, outdir: "Ui", settings: JsonSettings);
        }
    }

    JsonSerializerSettings JsonSettings => new() { ContractResolver = new AttributeAwareCamelCasePropertyNamesContractResolver() };
}