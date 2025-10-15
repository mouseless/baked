namespace Baked.Test.Documentation;

public class ShowingSwaggerUi : TestNfr
{
    protected override bool AllowAutoRedirect => true;

    [Test]
    public async Task Application_root_is_swagger_index_page()
    {
        var response = await Client.GetAsync("/");

        response.RequestMessage.ShouldNotBeNull();
        response.RequestMessage.RequestUri?.ToString().ShouldEndWith("/swagger/index.html");
    }
}