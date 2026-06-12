namespace Baked.Test.Localization;

public class LocalizeMessages : TestNfr
{
    [TestCase("en", "\"This is test message from English resource file.\"")]
    [TestCase("tr", "\"Bu Türkçe kaynak dosyasından test mesajıdır.\"")]
    public async Task Localization_reads_culture_information_from_header(string lang, string expected)
    {
        Client.DefaultRequestHeaders.Remove("Accept-Language");
        Client.DefaultRequestHeaders.Add("Accept-Language", lang);

        var response = await Client.GetAsync("/localization-samples/locale-string");
        var result = await response.Content.ReadAsStringAsync();

        result.ShouldBe(expected);
    }

    [TestCase("en", "\"This is test message from English resource file.\"")]
    [TestCase("tr", "\"Bu Türkçe kaynak dosyasından test mesajıdır.\"")]
    public async Task Localization_reads_culture_information_from_query(string lang, string expected)
    {
        var response = await Client.GetAsync($"/localization-samples/locale-string?culture={lang}");
        var result = await response.Content.ReadAsStringAsync();

        result.ShouldBe(expected);
    }

    [TestCase("en", "\"Parameter value is 'test'\"")]
    [TestCase("tr", "\"Parametre değeri 'test' verildi\"")]
    public async Task Variables_are_allowed_in_localized_messages(string lang, string expected)
    {
        var enResponse = await Client.GetAsync($"/localization-samples/parameterized?culture={lang}&param=test");
        var enResult = await enResponse.Content.ReadAsStringAsync();

        enResult.ShouldBe(expected);
    }

    [TestCase("en", "Invalid Request", "The field 'Parameter' is required.")]
    [TestCase("tr", "Geçersiz İstek", "'Parametre' alanı zorunludur.")]
    public async Task Model_validation_uses_request_language(string lang, string expectedTitle, string expectedMessage)
    {
        Client.DefaultRequestHeaders.Remove("Accept-Language");
        Client.DefaultRequestHeaders.Add("Accept-Language", lang);

        var response = await Client.GetAsync("/localization-samples/parameterized");
        dynamic? result = await response.Content.Deserialize();

        ((string?)result?.title).ShouldBe(expectedTitle);
        ((string?)result?.errors?.param?[0]).ShouldBe(expectedMessage);
    }

    [TestCase("en", "Invalid Request", "A value for 'Number' field was not provided.")]
    [TestCase("tr", "Geçersiz İstek", "'Sayı' alanı için bir değer verilmemiş.")]
    public async Task Model_binding_errors__missing_bind_required(string lang, string expectedTitle, string expectedMessage)
    {
        Client.DefaultRequestHeaders.Remove("Accept-Language");
        Client.DefaultRequestHeaders.Add("Accept-Language", lang);

        var response = await Client.GetAsync("/localization-samples/model-binding-errors");
        dynamic? result = await response.Content.Deserialize();

        ((string?)result?.title).ShouldBe(expectedTitle);
        ((string?)result?.errors?.number?[0]).ShouldBe(expectedMessage);
    }

    [TestCase("en", "Invalid Request", "The value 'invalid' is not valid for the field 'Number'.")]
    [TestCase("tr", "Geçersiz İstek", "'invalid' değeri 'Sayı' alanı için geçerli değil.")]
    public async Task Model_binding_errors__attempted_value_is_invalid__number(string lang, string expectedTitle, string expectedMessage)
    {
        Client.DefaultRequestHeaders.Remove("Accept-Language");
        Client.DefaultRequestHeaders.Add("Accept-Language", lang);

        var response = await Client.GetAsync("/localization-samples/model-binding-errors?number=invalid");
        dynamic? result = await response.Content.Deserialize();

        ((string?)result?.title).ShouldBe(expectedTitle);
        ((string?)result?.errors?.number?[0]).ShouldBe(expectedMessage);
    }

    [TestCase("en", "Invalid Request", "The value 'invalid' is not valid for the field 'Enumeration'.")]
    [TestCase("tr", "Geçersiz İstek", "'invalid' değeri 'Enumerasyon' alanı için geçerli değil.")]
    public async Task Model_binding_errors__attempted_value_is_invalid__enumeration(string lang, string expectedTitle, string expectedMessage)
    {
        Client.DefaultRequestHeaders.Remove("Accept-Language");
        Client.DefaultRequestHeaders.Add("Accept-Language", lang);

        var response = await Client.GetAsync("/localization-samples/model-binding-errors?number=1&enumeration=invalid");
        dynamic? result = await response.Content.Deserialize();

        ((string?)result?.title).ShouldBe(expectedTitle);
        ((string?)result?.errors?.enumeration?[0]).ShouldBe(expectedMessage);
    }
}