namespace Do.Test.Architecture.Layer;

public class ConfiguringLayers
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
    public void A_layer_can_make_a_configuration_context_do_stuff_before_and_after_being_configured_by_features() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void A_layer_can_make_a_configuration_context_do_stuff_before_and_after_phase_is_applied_to_layers_and_features() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Configuration_context_is_designed_to_have_extension_methods_to_be_used_by_features() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void A_configuration_context_applies_given_action_only_when_given_type_matches_current_target_type() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void A_layer_may_provide_a_configuration_context_up_to_three_targets() => Assert.Fail();
}

/*

 public class LayerA : LayerBase<PhaseX, PhaseY>
 {
    public LayerAConfigurationX ConfigX { get; } = new();
    public LayerAConfigurationY ConfigY { get; } = new();

    protected override ILayerContext GetContext(PhaseX phase, ApplicationContext context) =>
        LayerContext
            .Create(ConfigX)
            .OnBeforePhase(() => context
                .Get<IServiceCollection>()
                .AddLayerAStuff()
            )
            .OnAfterPhase(() => context
                .Get<IServiceCollection>()
                .ConfigureLayerA(ConfigX)
            );

    protected override ILayerContext GetContext(PhaseY phase, ApplicationContext context) =>
        LayerContext
            .Create(ConfigY)
            .OnAfter(() => context
                .Get<IApplicationBuilder>()
                .UseLayerA(ConfigY)
            );
 }

 */
