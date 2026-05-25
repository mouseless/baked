using Baked.Domain.Configuration;

namespace Baked.Test.Domain;

[TestFixture]
public class OrderingConventions : TestSpec
{
    static List<string> _values = default!;

    DomainModelPostBuilder _postBuilder = default!;
    DomainModelBuilderOptions _options = default!;

    public override void SetUp()
    {
        base.SetUp();

        _values = new();
        _options = new();
        _postBuilder = new(_options, GiveMe.TheDomainModel());
    }

    public class TestConvention(string _value) : IDomainModelConvention<TypeModelContext>
    {
        public void Apply(TypeModelContext model)
        {
            _values.Add(_value);
        }
    }

    [Test]
    public void Respecting_order_level()
    {
        _options.Conventions.Add(new TestConvention("C1"));
        _options.Conventions.Add(new TestConvention("C2"));
        _options.Conventions.Add(new TestConvention("C3"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C3");
        _values[1].ShouldBe("C1");
        _values[2].ShouldBe("C2");
    }
}