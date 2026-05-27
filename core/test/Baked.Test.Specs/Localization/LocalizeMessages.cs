namespace Baked.Test.Localization;

public class LocalizeMessages : TestNfr
{
    [Test]
    public async Task Localization_reads_culture_information_from_header()
    {
        Client.DefaultRequestHeaders.Add("Accept-Language", "en");

        var enResponse = await Client.GetAsync("/localization-samples/locale-string");
        var enResult = await enResponse.Content.ReadAsStringAsync();

        enResult.ShouldBe("\"This is test message from English resource file.\"");

        Client.DefaultRequestHeaders.Remove("Accept-Language");
        Client.DefaultRequestHeaders.Add("Accept-Language", "tr");

        var trResponse = await Client.GetAsync("/localization-samples/locale-string");
        var trResult = await trResponse.Content.ReadAsStringAsync();

        trResult.ShouldBe("\"Bu Türkçe kaynak dosyasından test mesajıdır.\"");
    }

    [Test]
    public async Task Localization_reads_culture_information_from_query()
    {
        var enResponse = await Client.GetAsync("/localization-samples/locale-string?culture=en");
        var enResult = await enResponse.Content.ReadAsStringAsync();

        enResult.ShouldBe("\"This is test message from English resource file.\"");

        var trResponse = await Client.GetAsync("/localization-samples/locale-string?culture=tr");
        var trResult = await trResponse.Content.ReadAsStringAsync();

        trResult.ShouldBe("\"Bu Türkçe kaynak dosyasından test mesajıdır.\"");
    }

    [Test]
    public async Task Variables_are_allowed_in_localized_messages()
    {
        var enResponse = await Client.GetAsync("/localization-samples/parameterized?culture=en&param=test");
        var enResult = await enResponse.Content.ReadAsStringAsync();

        enResult.ShouldBe("\"Parameter value is 'test'\"");

        var trResponse = await Client.GetAsync("/localization-samples/parameterized?culture=tr&param=test");
        var trResult = await trResponse.Content.ReadAsStringAsync();

        trResult.ShouldBe("\"Parametre değeri 'test' verildi\"");
    }

    [Test]
    public async Task Model_validation_uses_request_language()
    {
        Client.DefaultRequestHeaders.Add("Accept-Language", "en");

        var enResponse = await Client.GetAsync("/localization-samples/parameterized");
        dynamic? enResult = await enResponse.Content.Deserialize();

        ((string?)enResult?.title).ShouldBe("Invalid Request");
        ((int?)enResult?.errors.param.Count).ShouldBe(1);
        ((string?)enResult?.errors.param[0]).ShouldBe("The field 'Parameter' is required.");

        Client.DefaultRequestHeaders.Remove("Accept-Language");
        Client.DefaultRequestHeaders.Add("Accept-Language", "tr");

        var trResponse = await Client.GetAsync("/localization-samples/parameterized");
        dynamic? trResult = await trResponse.Content.Deserialize();

        ((string?)trResult?.title).ShouldBe("Geçersiz İstek");
        ((int?)trResult?.errors.param.Count).ShouldBe(1);
        ((string?)trResult?.errors.param[0]).ShouldBe("'Parametre' alanı zorunludur.");
    }
}