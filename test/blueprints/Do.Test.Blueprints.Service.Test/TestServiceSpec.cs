namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    static TestServiceSpec() =>
        Init(
            business: c => c.Default(),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? "42" : "test value";

    public void VerifyPersists(Entity entity)
    {
        var entities = GiveMe.The<Entities>();

        var result = entities.By(entity.String).FirstOrDefault(e => e.Guid == entity.Guid);

        result.ShouldNotBeNull();
    }
}