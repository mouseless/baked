namespace Baked.Test.Localization;

public class LocalizeMessages : TestServiceNfr
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
}