using Baked.Id;
using Baked.Id.Guid;

namespace Baked;

public static class GuidIdExtensions
{
    public static GuidIdFeature Guid(this IdConfigurator _) =>
        new();
}