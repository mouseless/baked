using Baked.Architecture;
using Baked.Communication;

namespace Baked;

public static class CommunicationExtensions
{
    public static void AddCommunication(this List<IFeature> features, Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>> configure) =>
        features.Add(configure(new()));
}