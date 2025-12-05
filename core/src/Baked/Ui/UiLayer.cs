using Baked.Architecture;
using Baked.CodeGeneration;
using Baked.Ui.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using static Baked.CodeGeneration.CodeGenerationLayer;

namespace Baked.Ui;

public class UiLayer : LayerBase<GenerateCode>
{
    public const int MinConventionOrder = -ConventionOrderLimit;
    public const int MaxConventionOrder = ConventionOrderLimit;

    static bool NoUi => Environment.GetCommandLineArgs().Contains("--no-ui");
    internal static bool WarnForNone => Environment.GetCommandLineArgs().Contains("--warn-for-none");

    readonly AppDescriptor _appDescriptor = new();
    readonly ComponentExports _componentExports = new();
    readonly LayoutDescriptors _layoutDescriptors = new();
    readonly PageDescriptors _pageDescriptors = new();
    readonly LocaleTemplate _localeTemplate = new();

    protected override PhaseContext GetContext(GenerateCode phase)
    {
        if (NoUi) { return phase.CreateEmptyContext(); }

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
            {{string.Join($",{Environment.NewLine}\t", componentExports)}}
        } from "#components";

        export {
            {{string.Join($",{Environment.NewLine}\t", componentExports)}}
        }
        """;
}