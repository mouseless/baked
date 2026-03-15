using Baked.Architecture;
using Baked.Business;
using Baked.Recipe;
using Baked.Recipe.DataSource;
using Baked.Testing;

namespace Baked;

public abstract class DataSourceSpec : Spec
{
    protected static void Init(
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        Action<DataSourceRecipe>? options = default
    )
    {
        var recipe = new DataSourceRecipe(business, ExecutionMode.Test);
        options?.Invoke(recipe);

        Init(recipe.Apply);
    }
}