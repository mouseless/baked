namespace Baked.Test.Localization;

public class LocalizeMessages : TestServiceNfr
{
    [Test]
    public async Task Localization_reads_culture_information_from_header()
    {
        Client.DefaultRequestHeaders.Add("Accept-Language", "en");

        var enResponse = await Client.PostAsync("localization-samples/get-locale-string", null);
        var enResult = await enResponse.Content.ReadAsStringAsync();

        enResult.ShouldBe("\"This is test message from English resource file.\"");

        Client.DefaultRequestHeaders.Add("Accept-Language", "tr");

        var trResponse = await Client.PostAsync("localization-samples/get-locale-string", null);
        var trResult = await trResponse.Content.ReadAsStringAsync();

        trResult.ShouldBe("\"Bu Türkçe kaynak dosyasından test mesajıdır.\"");
    }

    [Test]
    public async Task Localization_reads_culture_information_from_query()
    {
        var enResponse = await Client.PostAsync("localization-samples/get-locale-string?culture=en", null);
        var enResult = await enResponse.Content.ReadAsStringAsync();

        enResult.ShouldBe("\"This is test message from English resource file.\"");

        var trResponse = await Client.PostAsync("localization-samples/get-locale-string?culture=tr", null);
        var trResult = await trResponse.Content.ReadAsStringAsync();

        trResult.ShouldBe("\"Bu Türkçe kaynak dosyasından test mesajıdır.\"");
    }
}