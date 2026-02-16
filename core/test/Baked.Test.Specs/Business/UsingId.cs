using Baked.Business;

namespace Baked.Test.Business;

[TestFixture]
public class UsingId
{
    [TestCase(12, "12")]
    [TestCase("String", "String")]
    public void Id_can_be_created_from_various_types(object value, string expected)
    {
        var id = Id.Create(value);

        id.ToString().ShouldBe(expected);
    }

    [Test]
    public void New_id_uses_new_guid()
    {
        var id = Id.NewId();

        Guid.TryParse(id.ToString(), out var guid).ShouldBeTrue();
        guid.ToString().ShouldBe(id.ToString());
    }

    [Test]
    public void Id_implements_equatable_with_operator_overloads()
    {
        var a = Id.Create(1);
        var b = Id.Create("1");
        var c = Id.Create("2");

        a.Equals(b).ShouldBeTrue();
        a.Equals(c).ShouldBeFalse();

        (a == b).ShouldBeTrue();
        (a != b).ShouldBeFalse();

        (a == c).ShouldBeFalse();
        (a != c).ShouldBeTrue();
    }

    [Test]
    public void Id_implements_comparable_with_operator_overloads()
    {
        var a = Id.Create(1);
        var b = Id.Create("2");

        a.CompareTo(b).ShouldBeNegative();
        (a < b).ShouldBeTrue();
        (a <= b).ShouldBeTrue();
        (a >= b).ShouldBeFalse();
        (a > b).ShouldBeFalse();
    }

    [Test]
    public void Id_provides_empty_instance()
    {
        var actual = Id.Empty;

        actual.IsEmpty.ShouldBe(true);
        actual.ToString().ShouldBe(string.Empty);
    }

    [Test]
    public void Id_value_is_trimmed()
    {
        var actual = Id.Parse(" a ");

        actual.ShouldBe(Id.Parse("a"));
    }
}