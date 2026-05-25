using Baked.Domain;
using Baked.Domain.Configuration;

namespace Baked.Test.Domain;

[TestFixture]
public class ApplyingConventions : TestSpec
{
    static List<string> _values = default!;

    DomainModelPostBuilder _postBuilder = default!;
    DomainModelBuilderOptions _builderOptions = default!;
    IDomainModelConventionCollection _conventions = default!;

    public override void SetUp()
    {
        base.SetUp();

        _values = new();

        _conventions = new DomainModelConventionCollection(_builderOptions);
        _builderOptions = new();
        _postBuilder = new(_builderOptions, _conventions, GiveMe.TheDomainModel());
    }

    public override void TearDown()
    {
        base.TearDown();

        _values.Clear();
    }

    public class TestConvention(string _value) : IDomainModelConvention<TypeModelContext>
    {
        public void Apply(TypeModelContext model)
        {
            _values.Add(_value);
        }
    }

    [Test]
    public void Conventions_are_applied_based_on_added_order()
    {
        _conventions.Add(new TestConvention("C1"));
        _conventions.Add(new TestConvention("C2"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C1");
        _values[1].ShouldBe("C2");
    }

    [Test]
    public void Conventions_can_have_specific_orders()
    {
        _conventions.Add(new TestConvention("C1"), order: 2);
        _conventions.Add(new TestConvention("C2"), order: 1);

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C2");
        _values[1].ShouldBe("C1");
    }

    [Test]
    public void Conventions_can_have_level_based_orders()
    {
        _builderOptions.ConventionLevels.Add("A");
        _builderOptions.ConventionLevels.Add("B");

        _conventions.Add(new TestConvention("B"), order: Order.FromLevel("B"));
        _conventions.Add(new TestConvention("A"), order: Order.FromLevel("A"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
    }

    [Test]
    public void Levels_have_default__min_and_max_values()
    {
        _builderOptions.ConventionLevels.Add("A");

        _conventions.Add(new TestConvention("C2"), order: Order.FromLevel("A").Min);
        _conventions.Add(new TestConvention("C3"), order: Order.FromLevel("A").Max);
        _conventions.Add(new TestConvention("C1"), order: Order.FromLevel("A"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C2");
        _values[1].ShouldBe("C1");
        _values[1].ShouldBe("C3");
    }
}