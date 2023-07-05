namespace Do.Test.Architecture.Layer;

public class ProvidingConfiguration
{
    [Test]
    [Ignore("not implemented")]
    public void Every_layer_provides_a_configuration_context_per_phase() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void When_layer_provides_no_context_in_a_phase__then_it_is_skipped_for_that_phase() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Configuration_context_may_restrict_a_configuration_object_into_one_of_its_interfaces() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void When_configuration_context_has_mutiple_targets__they_are_applied_separately_in_the_given_order() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void A_layer_can_make_a_configuration_context_do_stuff_before_and_after_being_configured_by_features() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void A_layer_can_make_a_configuration_context_do_stuff_before_and_after_phase_is_applied_to_layers_and_features() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void A_layer_may_provide_a_configuration_context_up_to_three_targets() => Assert.Fail();
}

/*

 public class LayerA : LayerBase<PhaseX, PhaseY>
 {
    public LayerAConfigurationX ConfigX { get; } = new();
    public LayerAConfigurationY1 ConfigY1 { get; } = new();
    public LayerAConfigurationY2 ConfigY2 { get; } = new();
    public LayerAConfigurationZA ConfigZA { get; } = new();
    public LayerAConfigurationZB ConfigZB { get; } = new();

    protected override ILayerContext GetContext(PhaseX phase) =>
        LayerContext
            .Create(ConfigX)
            .OnBeforePhase(() => Context
                .Get<IServiceCollection>()
                .AddLayerAStuff()
            )
            .OnAfterPhase(() => context
                .Get<IServiceCollection>()
                .ConfigureLayerA(ConfigX)
            );

    protected override ILayerContext GetContext(PhaseY phase) =>
        LayerContext
            .Create(ConfigY1, ConfigY2)
            .OnAfter(() => Context
                .Get<IApplicationBuilder>()
                .UseLayerA(ConfigY1, ConfigY2)
            );

    protected override LayerContext GetContext(PhaseZ phase) =>
        LayerContext.CreateMany(
            LayerContext
                .Create(ConfigZA)
                .OnAfter(() => Context
                    .Get<IApplicationBuilder>()
                    .UseLayerA(ConfigZA)
                ),
            LayerContext
                .Create(ConfigZB)
                .OnAfter(() => Context
                    .Get<IApplicationBuilder>()
                    .UseLayerA(ConfigZB)
                )
        );
 }

 */
