using Do.Architecture;
using Do.Documentation;

namespace Do;

public static class DocumentationExtensions
{
    public static void AddDocumentation(this List<IFeature> source, Func<DocumentationConfigurator, IDocumentationFeature> configure) => source.Add((IFeature)configure(new()));
}
