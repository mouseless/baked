using Do.Documentation;
using Do.Documentation.Default;

namespace Do;

public static class DefaultDocumentationExtensions
{
    public static DefaultDocumentationFeature Default(this DocumentationConfigurator _) => new();
}
