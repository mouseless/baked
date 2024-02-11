using Do.Testing;
using Moq;
using Newtonsoft.Json;

namespace Do.Communication.Mock;

public class MockClientConfiguration
{
    public IMockCollection MockClientDescriptors { get; } = new MockCollection();

    public void AddClientSetup<T>(List<MockClientSetup> setups) where T : class
    {
        MockClientDescriptors.Add<IClient<T>>(
            singleton: true,
            setup: mock =>
            {
                foreach (var setup in setups)
                {
                    mock
                    .Setup(c => c.Send(It.Is<Request>(r => setup.Match(r))))
                    .ReturnsAsync(new Response(JsonConvert.SerializeObject(setup.Response)));
                }
            }
        );
    }
}
