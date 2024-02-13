using Do.Testing;
using Moq;
using Newtonsoft.Json;

namespace Do.Communication.Mock;

public class MockClientConfiguration
{
    public IMockCollection MockClientDescriptors { get; } = new MockCollection();

    public void ForClient<T>(MockClientSetup setup) where T : class => ForClient<T>([setup]);
    public void ForClient<T>(params MockClientSetup[] setups) where T : class
    {
        MockClientDescriptors.Add<IClient<T>>(
            singleton: true,
            setup: mock =>
            {
                foreach (var setup in setups)
                {
                    mock
                    .Setup(c => c.Send(It.Is<Request>(r => setup.When(r))))
                    .ReturnsAsync(new Response(JsonConvert.SerializeObject(setup.Response)));
                }
            }
        );
    }
}
