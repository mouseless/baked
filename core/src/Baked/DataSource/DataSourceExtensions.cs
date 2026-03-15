using Baked.Architecture;
using Baked.Business;
using Baked.DataSource;

namespace Baked;

public static class DataSourceExtensions
{
    extension(Bake bake)
    {
        public Application DataSource(
            FeatureFunc<BusinessConfigurator> business,
            Action<DataSourceRecipe.Run>? options = default
        )
        {
            var recipe = new DataSourceRecipe.Run(business);
            options?.Invoke(recipe);

            return bake.Application(recipe.Apply);
        }
    }
}