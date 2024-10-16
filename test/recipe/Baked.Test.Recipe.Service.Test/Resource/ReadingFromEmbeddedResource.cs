using Baked.Resource.Embedded;

namespace Baked.Test.Resource;

public class ReadingFromEmbeddedResource : ResourceReaderTemplateSpec<EmbeddedResourceReader>
{
    protected override string ResourcePath => "Baked.Test.Resource.EmbeddedResource.txt";
    protected override string ExpectedContent => "embedded resource content";
}