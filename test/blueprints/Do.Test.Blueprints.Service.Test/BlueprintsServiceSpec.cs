namespace Do.Test;

public class BlueprintsServiceSpec : ServiceSpec
{
    static BlueprintsServiceSpec() =>
        Init(
            business: c => c.Default(),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );
}