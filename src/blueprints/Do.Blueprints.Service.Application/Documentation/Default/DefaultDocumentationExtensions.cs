using Do.Documentation;
using Do.Documentation.Default;

namespace Do;

public static class DefaultDocumentationExtensions
{
    public static IDocumentationFeature Default(this DocumentationConfigurator _) => new DefaultDocumentationFeature();
}
