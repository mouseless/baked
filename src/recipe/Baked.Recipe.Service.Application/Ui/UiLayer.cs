using Baked.Architecture;
using Baked.CodeGeneration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.Ui;

public class UiLayer : LayerBase<GenerateCode>
{
    public AppDescriptor _appDescriptor = new();
    public ComponentExports _componentExports = new();
    public LayoutDescriptors _layoutDescriptors = new();
    public PageDescriptors _pageDescriptors = new();

    protected override PhaseContext GetContext(GenerateCode phase) =>
        phase.CreateContextBuilder()
            .Add(_appDescriptor)
            .Add(_componentExports)
            .Add(_layoutDescriptors)
            .Add(_pageDescriptors)
            .OnDispose(GenerateUiSchemas)
            .Build();

    void GenerateUiSchemas()
    {
        var files = Context.Get<IGeneratedFileCollection>();

        files.Add(name: "components", content: ComponentExports(_componentExports.Distinct()), extension: "js", outdir: "Ui");

        files.AddAsJson($"app", _appDescriptor, outdir: "Ui", settings: JsonSettings);

        foreach (var layout in _layoutDescriptors)
        {
            if (layout.Schema is not INamedComponentSchema schema) { continue; }

            files.AddAsJson($"{schema.Name}.layout", layout, outdir: "Ui", settings: JsonSettings);
        }

        foreach (var page in _pageDescriptors)
        {
            if (page.Schema is not INamedComponentSchema schema) { continue; }

            files.AddAsJson($"{schema.Name}.page", page, outdir: Path.Combine("Ui", schema.Route), settings: JsonSettings);
        }
    }

    JsonSerializerSettings JsonSettings => new()
    {
        ContractResolver = new AttributeAwareCamelCasePropertyNamesContractResolver(),
        Converters = [new StringEnumConverter()]
    };

    string ComponentExports(IEnumerable<string> componentExports) => $$"""
    import {
        {{string.Join($",{Environment.NewLine}\t", componentExports.Select(e => $"Lazy{e}"))}}
    } from "#components";

    export {
        {{string.Join($",{Environment.NewLine}\t", componentExports.Select(e => $"Lazy{e}"))}}
    }
    """;
}