using Baked.Architecture;
using Baked.Business;
using Baked.Testing;

namespace Baked.DataSource;

public abstract class DataSourceSpec : Spec
{
    protected static void Init(FeatureFunc<BusinessConfigurator> business,
        Action<DataSourceRecipe>? options = default
    )
    {
        var recipe = new DataSourceRecipe.Test(business);
        options?.Invoke(recipe);

        Init(recipe.Apply);
    }
}