using Baked.Core;

namespace Baked.Test.Core;

public class MailAddressValueType
{
    [Test]
    public void Null_does_not_parse()
    {
        MailAddress.TryParse(null, out var mailAddress).ShouldBeFalse();
        mailAddress.ShouldBe(default);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("user")]
    [TestCase("@host.com")]
    [TestCase("user@")]
    [TestCase("user@host")]
    [TestCase("user..user@host.com")]
    [TestCase("user.host.com")]
    [TestCase("user@user@host.com")]
    [TestCase("user@host:host")]
    [TestCase("user@[123.123.123.123]")]
    [TestCase("user@.host.com")]
    public void TryParse_returns_false_and_throw_exception_for_invalid(string invalid)
    {
        var actual = () => { MailAddress.Parse(invalid); };

        actual.ShouldThrow<FormatException>().Message.ShouldBe($"'{invalid}' is not in an expected format");
        MailAddress.TryParse(invalid, out var _).ShouldBeFalse();
    }

    [TestCase("user@host.com")]
    [TestCase("user@host.org")]
    [TestCase("user@host.org.tr")]
    [TestCase("user-@host.com")]
    [TestCase("-user@host.com")]
    [TestCase("user/user@host.com")]
    [TestCase("_user_user@host.com")]
    [TestCase("user.user@host.com")]
    [TestCase("user👤user@host.com")]
    [TestCase("user@1host.com")]
    [TestCase("1user@host.com")]
    public void TryParse_returns_true_for_valid(string valid)
    {
        MailAddress.TryParse(valid, out var actual).ShouldBeTrue();
        actual.ShouldBe(valid);
        MailAddress.Parse(valid).ShouldBe(valid);
    }

    [TestCase("UsEr@host.com", "user@host.com")]
    [TestCase("user@HOST.net", "user@host.net")]
    [TestCase("USER@host.org", "user@host.org")]
    public void UpperCased_value_returns_must_be_lowerCased(string value, string expected)
    {
        MailAddress.TryParse(value, out var actual).ShouldBeTrue();
        actual.ShouldBe(expected);
        MailAddress.Parse(value).ShouldBe(expected);
    }

    [Test]
    public void Supports_equals_operators()
    {
        var mail1 = MailAddress.Parse("test@mouseless.org");
        var mail2 = mail1;
        var mail3 = MailAddress.Parse("host@mouseless.org");

        (mail1 == mail2).ShouldBeTrue();
        (mail1 == mail3).ShouldBeFalse();
        (mail1 != mail2).ShouldBeFalse();
        (mail1 != mail3).ShouldBeTrue();
    }

    [Test]
    public void Supports_equals_operators_against_string()
    {
        var mail1 = MailAddress.Parse("test@mouseless.org");
        var strMail1 = mail1.ToString();
        var mail2 = "host@mouseless.org";

        (mail1 == strMail1).ShouldBeTrue();
        (mail1 == mail2).ShouldBeFalse();
        (mail1 != strMail1).ShouldBeFalse();
        (mail1 != mail2).ShouldBeTrue();
    }

    [Test]
    public void Supports_equals_operators_against_nullable()
    {
        MailAddress? mail1 = MailAddress.Parse("test@mouseless.org");
        MailAddress? mail2 = mail1;
        MailAddress? mail3 = MailAddress.Parse("host@mouseless.org");

        (mail1 == mail2).ShouldBeTrue();
        (mail1 == mail3).ShouldBeFalse();
        (mail1 != mail2).ShouldBeFalse();
        (mail1 != mail3).ShouldBeTrue();
    }
}