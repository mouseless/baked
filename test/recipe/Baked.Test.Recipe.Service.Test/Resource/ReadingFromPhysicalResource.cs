using Baked.Resource.Physical;

namespace Baked.Test.Resource;

public class ReadingFromPhysicalResource : ResourceReaderTemplateSpec<PhysicalResourceReader>
{
    protected override string ResourcePath => "\\Resource\\PhysicalResource.txt";
    protected override string ExpectedContent => "physical resource content";
}