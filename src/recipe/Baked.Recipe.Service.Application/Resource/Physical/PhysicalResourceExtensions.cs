using Baked.Resource;
using Baked.Resource.Physical;

namespace Baked;

public static class PhysicalResourceExtensions
{
    public static PhysicalResourceFeature Physical(this ResourceConfigurator _, List<string> roots) =>
        new(roots);
}