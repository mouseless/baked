using Baked.Architecture;
using Baked.Testing;

namespace Baked.DataSource;

public abstract class DataSourceSpec : Spec
{
    protected static void Init(FeatureFunc<BusinessFeature.Configurator> business,
        Action<DataSourceRecipe.Test>? options = default
    )
    {
        var recipe = new DataSourceRecipe.Test(business);
        options?.Invoke(recipe);

        Init(recipe.Apply);
    }
}