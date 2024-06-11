using Baked.Architecture;
using Baked.Communication;

namespace Baked;

public static class CommunicationExtensions
{
    public static void AddCommunication(this List<IFeature> source, Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>> configure) =>
        source.Add(configure(new()));
}