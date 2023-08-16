using Do.Architecture;

namespace Do;

public static class ForgeExtensions
{
    public static Application CustomService(this Forge forge) => 
        forge.Service(
            business: c => c.Default(),
            database: c => c.MySql().ForDevelopment(c.Sqlite()),
            configure: app => app.Features.AddConfigurationOverrider()
        );
}
