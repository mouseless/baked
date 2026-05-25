using Baked.Domain.Configuration;

namespace Baked.Test.Domain;

[TestFixture]
public class AppliyingConventions : TestSpec
{
    static List<string> _values = default!;

    DomainModelPostBuilder _postBuilder = default!;
    DomainModelBuilderOptions _builderOptions = default!;

    public override void SetUp()
    {
        base.SetUp();

        _values = new();
        _builderOptions = new();
        _postBuilder = new(_builderOptions, GiveMe.TheDomainModel());
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
        _builderOptions.Conventions.Add(new TestConvention("C1"));
        _builderOptions.Conventions.Add(new TestConvention("C2"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C1");
        _values[1].ShouldBe("C2");
    }

    [Test]
    public void Conventions_can_have_specific_orders()
    {
        _builderOptions.Conventions.Add(new TestConvention("C1"), order: 2);
        _builderOptions.Conventions.Add(new TestConvention("C2"), order: 1);

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C2");
        _values[1].ShouldBe("C1");
    }

    [Test]
    public void Conventions_can_have_level_based_orders()
    {
        _builderOptions.ConventionLevels.Add(new("A"));
        _builderOptions.ConventionLevels.Add(new("B"));

        _builderOptions.Conventions.Add(new TestConvention("C2"), order: builder => builder.Levels("A"));
        _builderOptions.Conventions.Add(new TestConvention("C1"), order: builder => builder.Levels("B"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C2");
        _values[1].ShouldBe("C1");
    }

    [Test]
    public void Levels_are_applied_after_default_order()
    {
        _builderOptions.ConventionLevels.Add(new("A"));

        _builderOptions.Conventions.Add(new TestConvention("C2"), order: builder => builder.Levels("A"));
        _builderOptions.Conventions.Add(new TestConvention("C3"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C3");
        _values[1].ShouldBe("C2");
    }

    [Test]
    public void Pre_levels_apply_before_default_order()
    {
        _builderOptions.ConventionLevels.Add(new("A"));
        _builderOptions.ConventionLevels.Add(new("B", Pre: true));

        _builderOptions.Conventions.Add(new TestConvention("C2"), order: builder => builder.Levels("A"));
        _builderOptions.Conventions.Add(new TestConvention("C3"), order: builder => builder.Levels("B"));
        _builderOptions.Conventions.Add(new TestConvention("C1"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C3");
        _values[1].ShouldBe("C1");
        _values[2].ShouldBe("C2");
    }

    [Test]
    public void Levels_have_default__min_and_max_values()
    {
        _builderOptions.ConventionLevels.Add(new("A"));

        _builderOptions.Conventions.Add(new TestConvention("C2"), order: builder => builder.Levels("A").Min);
        _builderOptions.Conventions.Add(new TestConvention("C3"), order: builder => builder.Levels("A").Max);
        _builderOptions.Conventions.Add(new TestConvention("C1"), order: builder => builder.Levels("A"));

        _postBuilder.EndBuild();

        _values[0].ShouldBe("C2");
        _values[1].ShouldBe("C1");
        _values[1].ShouldBe("C3");
    }
}