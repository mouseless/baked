using Humanizer;

namespace Baked.Core;

public class HumanizerTextTransformer : ITextTransformer
{
    public string Camelize(string text) =>
        text.Camelize();

    public string Kebaberize(string text) =>
        text.Kebaberize();

    public string Pascalize(string text) =>
        text.Pascalize();

    public string Pluralize(string text) =>
        text.Pluralize();

    public string Singularize(string text) =>
        text.Singularize();

    public string Snakerize(string text) =>
        text.Underscore();

    public string Titleize(string text) =>
        text.Titleize();
}