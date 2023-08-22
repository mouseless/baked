namespace Do.Test.Architecture.Application;

public class AddingExtensions : ArchitectureSpec
{
    [Test]
    public void Layer_is_added_without_any_options()
    {
        var forge = GiveMe.AForge();
        var layer1 = MockMe.ALayer();
        var layer2 = MockMe.ALayer();

        var app = forge.Application(app =>
        {
            app.Layers.Add(layer1);
            app.Layers.Add(layer2);
        });

        app.Run();

        layer1.VerifyInitialized();
        layer2.VerifyInitialized();
    }

    [Test]
    public void Feature_is_added_to_configure_layers()
    {
        var forge = GiveMe.AForge();
        var layer = MockMe.ALayer();
        var feature1 = MockMe.AFeature();
        var feature2 = MockMe.AFeature();

        var app = forge.Application(app =>
        {
            app.Layers.Add(layer);

            app.Features.Add(feature1);
            app.Features.Add(feature2);
        });

        app.Run();

        feature1.VerifyInitialized();
        feature2.VerifyInitialized();
    }

    [Test]
    public void Feature_configures_target_configurations_of_the_layers()
    {
        var forge = GiveMe.AForge();
        var layer = MockMe.ALayer(target: "text");
        var feature = MockMe.AFeature();

        var app = forge.Application(app =>
        {
            app.Layers.Add(layer);

            app.Features.Add(feature);
        });

        app.Run();

        feature.VerifyConfigures("text");
    }

    [Test]
    public void Layers_can_provide_multiple_configuration_targets()
    {
        var forge = GiveMe.AForge();
        var layer = MockMe.ALayer(targets: new object[] { "text", 10 });
        var feature = MockMe.AFeature();

        var app = forge.Application(app =>
        {
            app.Layers.Add(layer);

            app.Features.Add(feature);
        });

        app.Run();

        feature.VerifyConfigures("text");
        feature.VerifyConfigures(10);
    }

    [Test]
    public void Layers_are_skipped_when_they_provide_no_configuration_target()
    {
        var forge = GiveMe.AForge();
        var layer = MockMe.ALayer(targets: new object[0]);
        var feature = MockMe.AFeature();

        var app = forge.Application(app =>
        {
            app.Layers.Add(layer);

            app.Features.Add(feature);
        });

        app.Run();

        feature.VerifyConfiguresNothing();
    }

    [Test]
    public void Adding_the_same_layer_more_than_once_gives_error()
    {
        var forge = GiveMe.AForge();
        var layer = MockMe.ALayer();

        var forgeAction = () => forge.Application(app =>
        {
            app.Layers.Add(layer);
            app.Layers.Add(layer);
        });

        forgeAction.ShouldThrow<InvalidOperationException>().Message.ShouldBe(
            $"Cannot add `{layer.ToString() ?? layer.GetType().Name}`, it was already added.");
    }

    [Test]
    public void Adding_the_same_feature_more_than_once_gives_error()
    {
        var forge = GiveMe.AForge();
        var feature = MockMe.AFeature();

        var forgeAction = () => forge.Application(app =>
        {
            app.Features.Add(feature);
            app.Features.Add(feature);
        });

        forgeAction.ShouldThrow<InvalidOperationException>().Message.ShouldBe(
            $"Cannot add `{feature.ToString() ?? feature.GetType().Name}`, it was already added.");
    }
}
