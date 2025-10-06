namespace Baked.Core;

public interface ITextTransformer
{
    string Camelize(string text);
    string Kebaberize(string text);
    string Pascalize(string text);
    string Pluralize(string text);
    string Singularize(string text);
    string Snakerize(string text);
    string Titleize(string text);
}