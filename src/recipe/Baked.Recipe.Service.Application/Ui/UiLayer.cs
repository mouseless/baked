using Baked.Architecture;
using Baked.CodeGeneration;
using Newtonsoft.Json;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.Ui;

public class UiLayer : LayerBase<GenerateCode>
{
    public AppSettings _appSettings = new();
    public LayoutDescriptors _layoutDescriptors = new();
    public PageDescriptors _pageDescriptors = new();

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContextBuilder()
            .Add(_appSettings)
            .Add(_layoutDescriptors)
            .Add(_pageDescriptors)
            .OnDispose(GenerateUiSchemas)
            .Build();

    void GenerateUiSchemas()
    {
        var files = Context.Get<IGeneratedFileCollection>();
        foreach (var layout in _layoutDescriptors)
        {
            if (layout.Schema is not INamedComponentSchema schema) { continue; }

            files.AddAsJson($"{schema.Name}.layout", layout, outdir: "Ui", settings: JsonSettings);
        }

        foreach (var page in _pageDescriptors)
        {
            if (page.Schema is not INamedComponentSchema schema) { continue; }

            files.AddAsJson($"{schema.Name}.page", page, outdir: "Ui", settings: JsonSettings);
        }

        files.AddAsJson($"app.settings", _appSettings, outdir: "Ui", settings: JsonSettings);
    }

    JsonSerializerSettings JsonSettings => new() { ContractResolver = new AttributeAwareCamelCasePropertyNamesContractResolver() };
}