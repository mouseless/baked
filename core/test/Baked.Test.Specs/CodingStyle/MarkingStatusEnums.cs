using Baked.Business;
using Baked.Playground.CodingStyle.StatusEnumViaSuffix;
using Baked.Playground.Orm;

namespace Baked.Test.CodingStyle;

public class MarkingStatusEnums : TestSpec
{
    [Test]
    public void Enums_with_status_suffix_get_severities_using_their_values()
    {
        var severities = GiveMe.TheTypeModel<EntityStatus>().GetMetadata().Get<Severities>();

        severities.Value[nameof(EntityStatus.Info)].ShouldBe("info");
        severities.Value[nameof(EntityStatus.Success)].ShouldBe("success");
        severities.Value[nameof(EntityStatus.Warning)].ShouldBe("warn");
        severities.Value[nameof(EntityStatus.Error)].ShouldBe("danger");
    }

    [Test]
    public void Values_out_of_severity_ranges_are_skipped()
    {
        var severities = GiveMe.TheTypeModel<EntityStatus>().GetMetadata().Get<Severities>();

        severities.Value.ShouldNotContainKey(nameof(EntityStatus.Default));
    }

    [Test]
    public void Enums_with_an_underlying_type_other_than_int_are_supported()
    {
        var severities = GiveMe.TheTypeModel<LongBasedStatus>().GetMetadata().Get<Severities>();

        severities.Value[nameof(LongBasedStatus.Waiting)].ShouldBe("info");
        severities.Value[nameof(LongBasedStatus.Done)].ShouldBe("success");
        severities.Value[nameof(LongBasedStatus.Failed)].ShouldBe("danger");
    }

    [Test]
    public void Properties_of_status_enums_are_marked_as_status()
    {
        var entity = GiveMe.TheTypeModel<Entity>().GetMembers();

        entity.Properties[nameof(Entity.Status)].Has<Status>().ShouldBeTrue();
    }

    [Test]
    public void Nullable_properties_of_status_enums_are_marked_as_status()
    {
        var parent = GiveMe.TheTypeModel<Parent>().GetMembers();

        parent.Properties[nameof(Parent.Status)].Has<Status>().ShouldBeTrue();
    }
}