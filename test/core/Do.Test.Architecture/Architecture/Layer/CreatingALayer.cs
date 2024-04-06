using Do.Architecture;

namespace Do.Test.Architecture.Layer;

public class CreatingALayer : ArchitectureSpec
{
    public class LayerA : LayerBase { }

    [Test]
    public void A_layer_is_created_by_extending_LayerBase()
    {
        var layer = new LayerA();

        layer.ShouldBeAssignableTo<LayerBase>();
    }

    [Test]
    public void All_layer_classes_implement_ILayer_behind_the_scenes()
    {
        var layer = new LayerA();

        layer.ShouldBeAssignableTo<ILayer>();
    }

    [Test]
    public void Using_LayerBase_as_base_class__by_default__id_of_a_layer_is_its_class_name()
    {
        var layer = new LayerA() as ILayer;

        layer.Id.ShouldBe(nameof(LayerA));
    }

    public class LayerB : LayerBase
    {
        protected override string Id => "B Layer";
    }

    [Test]
    public void Layer_id_can_be_overriden_by_overriding_the_Id_property_in_layer_class()
    {
        var layer = new LayerB() as ILayer;

        layer.Id.ShouldBe("B Layer");
    }
}