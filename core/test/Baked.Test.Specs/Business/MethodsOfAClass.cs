using Baked.Test;
using System.Net;

namespace Baked.Business;

public class MethodsOfAClass : TestNfr
{
    [Test]
    public async Task Method_declared_in_class_is_an_endpoint()
    {
        var response = await Client.PostAsync("/class/method", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Method_declared_in_abstract_as_virtual_is_an_endpoint()
    {
        var response = await Client.PostAsync("/class/virtual-method", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Method_declared_in_abstract_as_abstract_and_overridden_in_class_is_an_endpoint()
    {
        var response = await Client.PostAsync("/class/abstract-method", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Method_declared_in_interface_and_implemented_in_class_is_an_endpoint()
    {
        var response = await Client.PostAsync("/class/interface-method", null);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Method_declared_in_base_other_than_business_assembly_is_NOT_an_endpoint()
    {
        var response = await Client.PostAsync("/class/to-string", null);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Method_declared_in_base_other_than_business_assembly_but_overridden_in_class_is_NOT_an_endpoint()
    {
        var response = await Client.GetAsync("/class/hash-code");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}