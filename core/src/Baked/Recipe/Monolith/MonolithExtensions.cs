using Baked.Architecture;
using Baked.Business;
using Baked.Recipe.Monolith;

namespace Baked;

public static class MonolithExtensions
{
    extension(Bake bake)
    {
        public Application Monolith(
            FeatureFunc<BusinessConfigurator> business,
            Action<MonolithRecipe>? options = default
        )
        {
            var recipe = new MonolithRecipe.Run(business);
            options?.Invoke(recipe);

            return bake.Application(recipe.Apply);
        }
    }
}