using Baked.Architecture;
using Baked.Communication;

namespace Baked;

public static class CommunicationExtensions
{
    extension(List<IFeature> features)
    {
        public void AddCommunication(Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>> configure) =>
            features.Add(configure(new()));
    }
}