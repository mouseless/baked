using Baked.Architecture;
using Baked.CodeGeneration;
using Baked.Ui.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.Ui;

public class UiLayer : LayerBase<GenerateCode>
{
    [return: NotNullIfNotNull(nameof(key))]
    public delegate string? NewLocaleKey(string? key);

    public AppDescriptor _appDescriptor = new();
    public ComponentExports _componentExports = new();
    public LayoutDescriptors _layoutDescriptors = new();
    public PageDescriptors _pageDescriptors = new();
    public LocaleTemplate _localeTemplate = new();

    protected override PhaseContext GetContext(GenerateCode phase)
    {
        Context.Add<NewLocaleKey>(LocaleKeyFactory);

        return phase.CreateContextBuilder()
            .Add(_appDescriptor)
            .Add(_componentExports)
            .Add(_layoutDescriptors)
            .Add(_pageDescriptors)
            .OnDispose(() =>
            {
                GenerateUiSchemas();
                Context.Add<ILocaleTemplate>(_localeTemplate);
            })
            .Build();
    }

    string? LocaleKeyFactory(string? key)
    {
        if (key is not null)
        {
            _localeTemplate[key] = key;
        }

        return key;
    }

    void GenerateUiSchemas()
    {
        var files = Context.Get<IGeneratedFileCollection>();
        files.Add("components", ComponentExportsTemplate(_componentExports.Distinct().Order()), extension: "js", outdir: "Ui");
        files.AddAsJson($"app", _appDescriptor, outdir: "Ui", settings: JsonSettings);

        foreach (var layout in _layoutDescriptors)
        {
            if (layout.Schema is not IGeneratedComponentSchema schema) { continue; }

            files.AddAsJson($"{schema.Name}.layout", layout, outdir: Path.Combine("Ui", schema.Dir), settings: JsonSettings);
        }

        foreach (var page in _pageDescriptors)
        {
            if (page.Schema is not IGeneratedComponentSchema schema) { continue; }

            files.AddAsJson($"{schema.Name}.page", page, outdir: Path.Combine("Ui", schema.Dir), settings: JsonSettings);
        }
    }

    JsonSerializerSettings JsonSettings => new()
    {
        ContractResolver = new AttributeAwareCamelCasePropertyNamesContractResolver(),
        Converters = [new StringEnumConverter()],
        NullValueHandling = NullValueHandling.Ignore,
    };

    string ComponentExportsTemplate(IEnumerable<string> componentExports) => $$"""
        import {
            {{string.Join($",{Environment.NewLine}\t", componentExports.Select(e => $"Lazy{e}"))}}
        } from "#components";

        export {
            {{string.Join($",{Environment.NewLine}\t", componentExports.Select(e => $"Lazy{e}"))}}
        }
        """;
}