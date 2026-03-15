using Baked.Architecture;
using Baked.Business;
using Baked.Recipe;
using Baked.Recipe.Monolith;
using Baked.Testing;

namespace Baked;

public abstract class MonolithSpec : Spec
{
    public class Enum<T> where T : notnull
    {
        public static IEnumerable<T> Values() =>
            Enum.GetValues(typeof(T)).Cast<int>().Where(it => it > 0).Cast<T>();
    }

    protected static void Init(
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        Action<MonolithRecipe>? options = default
    )
    {
        var recipe = new MonolithRecipe(business, ExecutionMode.Test);
        options?.Invoke(recipe);

        Init(recipe.Apply);
    }

    public override void SetUp()
    {
        base.SetUp();

        // overrides configuration mock in `MockCoreFeature` with below default value provider
        MockMe.TheConfiguration(defaultValueProvider: GetDefaultSettingsValue);
    }

    protected virtual string? GetDefaultSettingsValue(string key) =>
        "test value";
}