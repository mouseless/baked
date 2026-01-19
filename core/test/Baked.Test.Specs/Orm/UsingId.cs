namespace Baked.Test.Orm;

[TestFixture]
public class UsingId
{
    [TestCase(12, "12")]
    [TestCase("String", "String")]
    public void Id_can_be_created_from_various_types(object value, string expected)
    {
        var id = Baked.Business.Id.Parse(value);

        id.ToString().ShouldBe(expected);
    }

    [Test]
    public void Id_implements_equatable_with_operator_overloads()
    {
        var a = Baked.Business.Id.Parse(1);
        var b = Baked.Business.Id.Parse("1");
        var c = Baked.Business.Id.Parse("2");

        a.Equals(b).ShouldBeTrue();
        a.Equals(c).ShouldBeFalse();
        (a == b).ShouldBeTrue();
        (a != b).ShouldBeFalse();
    }
}