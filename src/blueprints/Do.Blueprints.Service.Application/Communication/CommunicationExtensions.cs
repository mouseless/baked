using Do.Architecture;
using Do.Communication;

namespace Do;

public static class CommunicationExtensions
{
    public static void AddCommunication(this List<IFeature> source, Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>> configure) =>
        source.Add(configure(new()));
}
