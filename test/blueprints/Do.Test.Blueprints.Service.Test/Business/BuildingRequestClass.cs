using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Do.Test.Business;

public class BuildingRequestClass : TestServiceNfr
{
    [Test]
    public async Task Builds_request_class_from_method_paramaters()
    {
        var response = await Client.PostAsync($"/method-samples/request-class", JsonContent.Create(
            new
            {
                @string = "string",
                @int = 1,
                @object = new
                {
                    text = "text",
                    numeric = 2
                }
            }
        ));
        var actual = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        actual.ShouldDeeplyBe(
           new
           {
               @string = "string",
               @int = 1,
               @object = new
               {
                   text = "text",
                   numeric = 2
               }
           }
        );
    }
}