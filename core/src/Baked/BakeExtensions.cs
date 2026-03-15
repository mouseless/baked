using Baked.Architecture;
using Baked.Business;
using Baked.Recipe.DataSource;
using Baked.Recipe.Monolith;

namespace Baked;

public static class BakeExtensions
{
    extension(Bake bake)
    {
        public Application Monolith(
            Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
            Action<MonolithRecipe>? options = default
        )
        {
            var recipe = new MonolithRecipe(business);
            options?.Invoke(recipe);

            return bake.Application(recipe.Apply);
        }

        public Application DataSource(
            Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
            Action<DataSourceRecipe>? options = default
        )
        {
            var recipe = new DataSourceRecipe(business);
            options?.Invoke(recipe);

            return bake.Application(recipe.Apply);
        }
    }
}